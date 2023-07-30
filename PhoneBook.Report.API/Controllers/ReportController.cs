using Microsoft.AspNetCore.Mvc;
using PhoneBook.Report.API.Services;

namespace PhoneBook.Report.API.Controllers
{
    [ApiController]
    [Route("api/v1/report")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService reportService;
        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await reportService.GetAllAsync();
            return Ok(result);
        }

        //[HttpGet("Get/{id}")]
        //public async Task<IActionResult> Get(string id)
        //{
        //    Guid record_id;
        //    if (!Guid.TryParse(id, out record_id))
        //        return BadRequest("Invalid id");

        //    var result = await reportService.GetAsync(record_id);
        //    return Ok(result);
        //}

        [HttpPost("RequestReport")]
        public async Task<IActionResult> RequestReport(string location)
        {
            await reportService.RequestReportAsync(location);
            return Ok();
        }
    }
}