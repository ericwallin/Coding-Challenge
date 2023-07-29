using ClarkCodingChallenge.BusinessLogic;
using ClarkCodingChallenge.DataAccess;
using ClarkCodingChallenge.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.Controllers
{
    public class ContactController : Controller
    {
        // When the user submits this data, the input should be validated against the following rules.Any validation errors should be presented to the user.
        // * First Name cannot be empty
        // * Last Name cannot be empty
        // * Email must be a valid email address.
        // * If all validation succeeds, a confirmation page should be displayed which tells the user the data was received by the system.

        private readonly IContactsService _contactsService;

        public ContactController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _contactsService.SaveContactAsync(
                    new Contact
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        EmailAddress = model.EmailAddress
                    }
                );

                return View("Confirmation", model);
            }
            catch
            {
                Response.StatusCode = 500;
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
