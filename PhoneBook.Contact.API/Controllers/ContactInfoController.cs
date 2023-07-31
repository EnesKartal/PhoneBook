using Microsoft.AspNetCore.Mvc;
using PhoneBook.Contact.API.Models.DTO.ContactInfo.AddContactInfoRequestDTO;
using PhoneBook.Contact.API.Services;

namespace PhoneBook.Contact.API.Controllers
{
    [ApiController]
    [Route("api/v1/contactinfo")]
    public class ContactInfoController : ControllerBase
    {
        private readonly IContactInfoService contactInfoService;
        public ContactInfoController(IContactInfoService contactInfoService)
        {
            this.contactInfoService = contactInfoService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddContactInfoRequestDTO request)
        {
            var result = await contactInfoService.AddAsync(request);
            return Ok(result);
        }

        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            Guid record_id;
            if (!Guid.TryParse(id, out record_id))
                return BadRequest("Invalid id");

            await contactInfoService.RemoveAsync(record_id);
            return Ok();
        }
    }
}
