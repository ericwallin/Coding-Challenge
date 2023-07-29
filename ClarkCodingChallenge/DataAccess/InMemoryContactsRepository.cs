using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.DataAccess
{
    /*
     * All data submitted in this way must be saved, but only for the duration of the application’s run.
     * 
     * Eric: Sqlite in-memory database is ephermeral, so it will be destroyed when the application is closed.
     * 
     * It does not need to survive a restart of the application.
     * 
     * Eric: It won't :)
     * 
     * However, in your design consider that this application will ultimately have to save this data to a persistent datastore.
     * 
     * Eric: Use another implementation of IContactsRepostiory and use dependency injection to swap out the implementation.
     * 
     */

    public class InMemoryContactsRepository : IContactsRepository
    {
        private readonly static string _connectionString = "Data Source=ContactsDB;Mode=Memory;Cache=Shared";

        private readonly SqliteConnection _connection;

        public InMemoryContactsRepository()
        {
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
            SQLitePCL.Batteries.Init();

            _connection = new SqliteConnection(_connectionString);
            _connection.Open();
            _connection.Execute("CREATE TABLE Contacts (Id INTEGER PRIMARY KEY AUTOINCREMENT, FirstName TEXT, LastName TEXT, EmailAddress TEXT);");

            // choosing not to permit duplicate email addresses, ignoring case
            _connection.Execute("CREATE UNIQUE INDEX IX_Contacts_EmailAddress ON Contacts (EmailAddress COLLATE NOCASE);");
        }

        ~InMemoryContactsRepository()
        {
            // kids, don't try this at home. Needed to hold on to a connection for sqlite in-memory database to keep context alive.
            _connection.Close();
            _connection.Dispose();
        }

        public async Task SaveContactAsync(string firstName, string lastName, string emailAddress)
        {
            const string queryBase = "INSERT INTO Contacts (FirstName, LastName, EmailAddress) VALUES (@FirstName, @LastName, @EmailAddress)";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(queryBase, new { FirstName = firstName, LastName = lastName, EmailAddress = emailAddress });
                await connection.CloseAsync();
            }
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync(string lastName = "", bool sortAscending = true)
        {
            const string queryBase = "SELECT Id, FirstName, LastName, EmailAddress FROM Contacts";
            string queryByLastName = $"{queryBase} WHERE lower(LastName) = lower(@LastName) ORDER BY FirstName COLLATE NOCASE { (sortAscending ? "ASC" : "DESC") }";
            string queryAllNames = $"{queryBase} ORDER BY LastName COLLATE NOCASE {(sortAscending ? "ASC" : "DESC") }, FirstName COLLATE NOCASE {(sortAscending ? "ASC" : "DESC") }";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                if (!string.IsNullOrWhiteSpace(lastName))
                    return await connection.QueryAsync<Contact>(queryByLastName, new { LastName = lastName });

                return await connection.QueryAsync<Contact>(queryAllNames);
            }
        }
    }
}
