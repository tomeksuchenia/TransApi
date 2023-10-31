using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trans.Infrastructure.Commands.Companies;
using Trans.Infrastructure.Dto;
using Trans.Infrastructure.Services;

namespace Trans.Api.Controllers
{
    [Route("companies/orders")]
    public class CompaniesOrdersController : ApiControllerBase
    {
        private readonly ICompanyOrderService _companyOrderService;

        public CompaniesOrdersController(ICompanyOrderService companyOrderService)
        {
            _companyOrderService = companyOrderService;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            var companyId = UserId;
            var order = await _companyOrderService.GetOrderAsync(companyId, id);
            return Ok(order);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get()
        {
            var comapnyId = UserId;
            var orders = await _companyOrderService.BrowseAsync(comapnyId);
            return Ok(orders);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]CreateOrder command)
        {
            var companyId = UserId;
            await _companyOrderService.AddOrder(companyId, command.Load.NameLoads, command.Load.Weight, command.Load.Width, command.Load.Length,
                command.Load.Height, command.Load.Description, command.OrderCompany.Adress.Country, command.OrderCompany.Adress.City,
                command.OrderCompany.Adress.PostalCode, command.OrderCompany.Adress.Street, command.OrderCompany.Adress.BuildingNumber,
                command.OrderCompany.Name, command.OrderCompany.TelephoneNumber, command.PaymentDays, command.IsPaid, command.StatusOrderDescription, command.StartDayTransport,
                command.EndDayTransport);

            return Ok();
        }

        [Authorize]
        [HttpPut("{id}/paid")]
        public async Task<ActionResult> Post(int id, [FromQuery] bool isPaid)
        {
            var companyId = UserId;
            await _companyOrderService.UpdatePaidOrderAsync(companyId, id, isPaid);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Post(int id)
        {
            var companyId = UserId;
            await _companyOrderService.DeleteOrderAsync(companyId, id);
            return NoContent();
        }


    }
}
