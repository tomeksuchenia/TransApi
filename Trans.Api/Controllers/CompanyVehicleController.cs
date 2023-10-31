using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trans.Infrastructure.Commands.Companies;
using Trans.Infrastructure.Dto;
using Trans.Infrastructure.Services;

namespace Trans.Api.Controllers
{
    [Route("companies/vehicles")]
    public class CompanyVehicleController : ApiControllerBase
    {
        private readonly ICompanyVehicleService _companyVehicleService;

        public CompanyVehicleController(ICompanyVehicleService companyVehicleService)
        {
            _companyVehicleService = companyVehicleService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> Get()
        {
            var companyId = UserId;
            var vehicles = await _companyVehicleService.BrowseAsync(companyId);
            return Ok(vehicles);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]CreateVehicle command)
        {
           var companyId = UserId;
           await _companyVehicleService.AddAsync(companyId, command.Brand, command.Model, command.MaxVehicleWeight);
           return NoContent();
        }
        [Authorize]
        [HttpDelete("{vehicleId}")]
        public async Task<ActionResult> Delete(Guid vehicleId)
        {
            await _companyVehicleService.RemoveAsync(UserId, vehicleId);
            return NoContent();
        }
    }
}
