using Microsoft.AspNetCore.Mvc;
using PhoneBook.Contact.API.Models.DTO.Contact.AddContact;
using PhoneBook.Contact.API.Services;

namespace PhoneBook.Contact.API.Controllers
{
    [ApiController]
    [Route("api/v1/contact")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            this._contactService = contactService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _contactService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Guid record_id;
            if (!Guid.TryParse(id, out record_id))
                return BadRequest("Invalid id");

            var result = await _contactService.GetAsync(record_id);
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddContactRequestDTO request)
        {
            var result = await _contactService.AddAsync(request);
            return Ok(result);
        }

        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            Guid record_id;
            if (!Guid.TryParse(id, out record_id))
                return BadRequest("Invalid id");

            await _contactService.RemoveAsync(record_id);
            return Ok();
        }
    }
}
