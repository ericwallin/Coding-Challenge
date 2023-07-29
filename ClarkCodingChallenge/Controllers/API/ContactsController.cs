using ClarkCodingChallenge.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService _contactsService;
        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string lastName = "", bool sortAscending = true)
        {
            return new OkObjectResult(await _contactsService.GetContactsAsync(lastName, sortAscending));
        }
    }
}
