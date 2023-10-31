using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trans.Infrastructure.Commands.Companies;
using Trans.Infrastructure.Dto;
using Trans.Infrastructure.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Trans.Api.Controllers
{
    [Route("companies/drivers")]
    public class CompanyDriverController : ApiControllerBase
    {
        private readonly ICompanyDriverService _companyDriverService;

        public CompanyDriverController(ICompanyDriverService companyDriverService)
        {
            _companyDriverService = companyDriverService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverDto>>> Get()
        {
            var companyId = UserId;
            var drivers = await _companyDriverService.GetAsync(companyId);
            return Ok(drivers);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateDriver command)
        {
            await _companyDriverService.AddAsync(UserId, command.Name, command.Fullname, command.TelephoneNumer);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{driverId}")]
        public async Task<ActionResult> Delete(Guid DriverId)
        {
            var companyId = UserId;
            await _companyDriverService.RemoveAsync(companyId, DriverId);
            return NoContent();
        }
    }
}
