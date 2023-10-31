using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Core.Domain;
using Trans.Core.Repositories;
using Trans.Infrastructure.Dto;
using Trans.Infrastructure.Extensions;

namespace Trans.Infrastructure.Services
{
    public class CompanyOrderService : ICompanyOrderService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyOrderService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public async Task<OrderDto> GetOrderAsync(Guid companyId, int id)
        {
            var company = await _companyRepository.GetOrFail(companyId);
            var order = company.GetOrder(id);

            return _mapper.Map<OrderDto>(order);
        }
        public async Task<IEnumerable<OrderDto>> BrowseAsync(Guid companyId)
        {
            var company = await _companyRepository.GetOrFail(companyId);
            var orders = company.Orders;
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task AddOrder(Guid companyId,
            string nameLoads, int weight, int width, int length, int height, string description,
            string country, string city, string postalCode, string street, string buildingNumber,
            string name, string telephoneNumber,
            int paymentDays, bool isPaid, string statusOrderDescription, DateTime startDayTransport, DateTime endDayTransport)
        {
            var company = await _companyRepository.GetOrFail(companyId);
            var load = Load.Create(nameLoads, weight,width,length,height,description);
            var adress = Adress.Create(country, city, postalCode, street, buildingNumber);
            var orderCompany = OrderCompany.Create(name, adress, telephoneNumber);
            var order = new Order(orderCompany, load, paymentDays, isPaid, statusOrderDescription, startDayTransport, endDayTransport);
            company.AddOrder(order);
            await _companyRepository.UpdateAsync(company);
        }

        public async Task DeleteOrderAsync(Guid companyId, int orderId)
        {
            var company = await _companyRepository.GetOrFail(companyId);
            company.DeleteOrder(orderId);
            await _companyRepository.UpdateAsync(company);
        }

        public async Task UpdatePaidOrderAsync(Guid companyId, int orderId, bool isPaid)
        {
            var company = await _companyRepository.GetOrFail(companyId);
            company.SetPaidStatus(orderId, isPaid);
            await _companyRepository.UpdateAsync(company);
        }

    }
}
