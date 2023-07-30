using Microsoft.AspNetCore.Mvc;
using PhoneBook.Report.API.Services;

namespace PhoneBook.Report.API.Controllers
{
    [ApiController]
    [Route("api/v1/reportdetail")]
    public class ReportDetailController : ControllerBase
    {
        private readonly IReportDetailService reportDetailService;
        public ReportDetailController(IReportDetailService reportDetailService)
        {
            this.reportDetailService = reportDetailService;
        }

        [HttpGet("Get/{reportId}")]
        public async Task<IActionResult> Get(string reportId)
        {
            Guid record_id;
            if (!Guid.TryParse(reportId, out record_id))
                return BadRequest("Invalid id");

            var result = await reportDetailService.GetByReportIdAsync(record_id);
            return Ok(result);
        }
    }
}