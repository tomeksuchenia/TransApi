using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Trans.Infrastructure.Commands;
using Trans.Infrastructure.Commands.Users;
using Trans.Infrastructure.Dto;
using Xunit;

namespace TestsEndToEnd
{
    public class CompaniesControllerTests : TestsControllerBase<Program>
    {
        [Fact]
        public async Task company_should_be_created_throught_logged_user()
        {
            //arrange
            var company = new CreateCompany
            {
                Name = "TestCompany",
                Email = "test@email.com",
                TaxNumber = "123",
                Country = "Poland",
                City = "Test",
                PostalCode = "123",
                Street = "TestStreet",
                BuildingNumber = "31"
            };
            var loginCredentials = LoginUser("test1@email.com", "secret");
            var payloadLogin = MakePayload(loginCredentials);
            var payloadCompany = MakePayload(company);
            //act
            var respondLogin = await Client.PostAsync("users/account/login", payloadLogin);
            var responContentLogin = await respondLogin.Content.ReadAsStringAsync();
            var jwtDto = JsonConvert.DeserializeObject<JwtDto>(responContentLogin);
            GetAuthentication(jwtDto.Token);
            var respond = await Client.PostAsync("companies", payloadCompany);
            var user = await GetUserAsync("test1@email.com");
            var companyId = user.Id;
            //assert
            Assert.Equal(HttpStatusCode.Created, respond.StatusCode);
            Assert.Equal($"/companies/{companyId}", respond.Headers.Location.ToString());
        }

        private Login LoginUser(string email, string password)
        {
            return new Login
            {
                Email = email,
                Password = password
            };
        }

        private async Task<UserDto> GetUserAsync(string email)
        {
            var respond = await Client.GetAsync($"users/{email}");
            var stringContent = await respond.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserDto>(stringContent);
            return user;
        }


    }

}
