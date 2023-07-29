using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.DataAccess
{
    public interface IContactsRepository
    {
        Task SaveContactAsync(string firstName, string lastName, string emailAddress);
        Task<IEnumerable<Contact>> GetContactsAsync(string lastName = "", bool sortAscending = true);
    }
}
