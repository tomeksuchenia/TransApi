using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Core.Domain;
using Trans.Infrastructure.Dto;

namespace Trans.Infrastructure.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<Company, CompanyDto>();
                cfg.CreateMap<Driver, DriverDto>();
                cfg.CreateMap<Vehicle, VehicleDto>();
                cfg.CreateMap<Company, CompanyDetailsDto>();
                cfg.CreateMap<Adress, AdressDto>();
                cfg.CreateMap<Order, OrderDto>();
                cfg.CreateMap<Load, LoadDto>();
                cfg.CreateMap<OrderCompany, OrderCompanyDto>();
            })
            .CreateMapper(); 
    }
}
