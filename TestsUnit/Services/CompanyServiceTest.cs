using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Core.Domain;
using Trans.Core.Repositories;
using Trans.Infrastructure.Dto;
using Trans.Infrastructure.Services;
using Xunit;

namespace Tests.Services
{
    public class CompanyServiceTest
    {
        [Fact]
        public async Task when_invoke_get_async_this_should_invoke_get_async_from_company_repository()
        {
            //arange
            var adress = Adress.Create("TestCountry", "testCity", "11-212", "testStreet", "12");
            var company = new Company(Guid.NewGuid(), "company", "company@test.com", adress, "123456789");

            var companyDto = new CompanyDetailsDto
            {
                Id = company.Id,
            };
            var companyRepositoryMock = new Mock<ICompanyRepository>();
            var mapperMock = new Mock<IMapper>();

            var companyService = new CompanyService(companyRepositoryMock.Object, mapperMock.Object);
            companyRepositoryMock.Setup(x => x.GetAsync(company.Id)).ReturnsAsync(company);
            mapperMock.Setup(x => x.Map<CompanyDetailsDto>(company)).Returns(companyDto);
            //act
            companyService.GetAsync(company.Id);
            //assert
            companyRepositoryMock.Verify(x => x.GetAsync(company.Id), Times.Once());
            mapperMock.Verify(x => x.Map<CompanyDetailsDto>(company), Times.Once());

        }

        [Fact]
        public async Task when_invoke_get_async_companydto_should_be_return()
        {
            //arange
            var adress = Adress.Create("TestCountry", "testCity", "11-212", "testStreet", "12");
            var company = new Company(Guid.NewGuid(), "company", "company@test.com", adress, "123456789");

            var companyDto = new CompanyDetailsDto
            {
                Id = company.Id,
            };

            var companyRepositoryMock = new Mock<ICompanyRepository>();
            var mapperMock = new Mock<IMapper>();

            var companyService = new CompanyService(companyRepositoryMock.Object, mapperMock.Object);

            companyRepositoryMock.Setup(x => x.GetAsync(company.Id)).ReturnsAsync(company);
            mapperMock.Setup(x => x.Map<CompanyDetailsDto>(company)).Returns(companyDto);

            //act 

            var companyReturn = await companyService.GetAsync(company.Id);

            //assert 

            Assert.Equal(companyDto, companyReturn);
        }

        [Fact]
        public async Task register_async_should_invoke_add_async_on_repository()
        {
            //Arange
            var companyRepositoryMock = new Mock<ICompanyRepository>();
            var mapperMock = new Mock<IMapper>();

            var companyService = new CompanyService(companyRepositoryMock.Object, mapperMock.Object);

            //Act
            companyService.RegisterAsync(Guid.NewGuid(), "test", "test@email.com", "123", "test", "test", "2211", "test", "25");
            //Assert
            companyRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Company>()), Times.Once());
        }
    }
}
