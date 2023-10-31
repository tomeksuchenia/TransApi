using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trans.Infrastructure.Commands;
using Trans.Infrastructure.Dto;
using Trans.Infrastructure.Services;

namespace Trans.Api.Controllers
{
    [Route("companies")]
    public class CompaniesController : ApiControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> Get()
        {
            var companies = await _companyService.BrowseAsync();
            return Ok(companies);
        }

        [HttpGet("{companyId}")]
        public async Task<ActionResult<CompanyDetailsDto>> Get(Guid companyId)
        {
            var company = await _companyService.GetAsync(companyId);
            return Ok(company);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateCompany command)
        {
            var companyId = UserId;
            await _companyService.RegisterAsync(companyId, command.Name, command.Email, command.TaxNumber, command.Country, command.City,
                command.PostalCode, command.Street, command.BuildingNumber);
            return Created($"/companies/{companyId}", null);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("companyId")]
        public async Task<IActionResult> Delete(Guid companyId)
        {
            await _companyService.DeleteAsync(companyId);
            return NoContent();
        }

        
    }
}
