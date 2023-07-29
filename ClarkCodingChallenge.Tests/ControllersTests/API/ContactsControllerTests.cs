using ClarkCodingChallenge.BusinessLogic;
using ClarkCodingChallenge.Controllers.API;
using ClarkCodingChallenge.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.Tests.ControllersTests.API
{
    [TestClass]
    public class ContactsControllerTests
    {
        [TestMethod]
        public async Task Get_ReturnsOkResult_WithContacts()
        {
            var contactsService = Substitute.For<IContactsService>();
            contactsService.GetContactsAsync(Arg.Any<string>(), Arg.Any<bool>())
                .Returns(Task.FromResult((IEnumerable<Contact>) new List<Contact> { new Contact { FirstName = "Test1", LastName = "User", EmailAddress = "testuser1@example.com" } }));

            var controller = new ContactsController(contactsService);

            // result will be null if not http status 200 / OkObjectResult
            var result = await controller.Get("", true) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(1, ((IEnumerable<Contact>)result.Value).Count());
        }

        // if I hadn't used the database to do filtering and sorting, I would have some filtering / sorting tests here.
        // I trust that Sqlite's sorting and filtering works.

    }
}
