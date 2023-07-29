using ClarkCodingChallenge.BusinessLogic;
using ClarkCodingChallenge.DataAccess;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Linq;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.Tests.BusinessLogicTests
{
    [TestClass]
    public class ContactsBusinessLogicTests
    {
        [TestMethod]
        public async Task SaveContactAsync_ShouldCallRepository()
        {
            var contact = new Contact { FirstName = "Test", LastName = "User", EmailAddress = "test@example.com" };
            var contactsRepo = Substitute.For<IContactsRepository>();
            var contactsService = new ContactsService(contactsRepo, Substitute.For<ILogger<ContactsService>>());

            await contactsService.SaveContactAsync(contact);

            await contactsRepo.Received().SaveContactAsync(contact.FirstName, contact.LastName, contact.EmailAddress);
        }

        // filtering and sorting is done by database, so only testing for presense of returned records
        // need integration tests for actual results populated into test database

        [TestMethod]
        public async Task GetContactAsync_ShouldReturnResults()
        {
            var contactsRepo = Substitute.For<IContactsRepository>();

            contactsRepo.GetContactsAsync(Arg.Any<string>(), Arg.Any<bool>()).Returns(new Contact[]
                {
                    new Contact { FirstName = "Test1", LastName = "User1", EmailAddress = "test1@example.com"},
                    new Contact { FirstName = "Test2", LastName = "User2", EmailAddress = "test2@example.com"},
                    new Contact { FirstName = "Test3", LastName = "User3", EmailAddress = "test3@example.com"},
                });
            
            var contactsService = new ContactsService(contactsRepo, Substitute.For<ILogger<ContactsService>>());

            Assert.AreEqual((await contactsService.GetContactsAsync()).Count(), 3);
        }
    }
}
