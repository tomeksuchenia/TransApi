using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Core.Domain;
using Trans.Infrastructure.Dto;

namespace Trans.Infrastructure.Services
{
    public interface ICompanyOrderService
    {
        Task<OrderDto> GetOrderAsync(Guid companyId, int id);
        Task<IEnumerable<OrderDto>> BrowseAsync(Guid companyId);
        Task AddOrder(Guid companyId,
            string nameLoads, int weight, int width, int length, int height, string description,
            string country, string city, string postalCode, string street, string buildingNumber,
            string name, string telephoneNumber,
            int paymentDays, bool isPaid, string statusOrderDescription, DateTime startDayTransport, DateTime endDayTransport);
        Task UpdatePaidOrderAsync(Guid companyId, int orderId, bool isPaid);

        Task DeleteOrderAsync(Guid companyId, int orderId);

    }
}
