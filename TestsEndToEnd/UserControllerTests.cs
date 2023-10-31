using System.Net;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Net.Http;
using TestsEndToEnd;
using Trans.Infrastructure.Commands;
using Newtonsoft.Json;
using System.Text;
using Trans.Infrastructure.Dto;
using Trans.Infrastructure.Commands.Users;
using System.Net.Http.Headers;

namespace TestsEndToEnd1
{
    public class UserControllerTests : TestsControllerBase<Program>
    {

        [Fact]
        public async Task given_invalid_email_user_should_be_not_exist()
        {
            //arrange
            var email = "user1000@email.com";
            //act
            var response = await Client.GetAsync($"users/{email}");
            //assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task given_end_point_users_status_code_should_be_Ok()
        {
            //arrange
            //act
            var resposne = await Client.GetAsync($"users");
            //asert
            Assert.Equal(HttpStatusCode.OK, resposne.StatusCode);
            
        }

        [Fact]
        public async Task given_valid_email_user_should_be_exist()
        {
            //arrange
            var email = "test1@email.com";
            //act
            var response = await Client.GetAsync($"users/{email}");
            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task given_unique_email_user_should_be_created()
        {
            //arrange
            var createUser = CreateNewUser("user@test10.com");
            //act
            var payload = MakePayload(createUser);
            var response = await Client.PostAsync("users/register", payload);
            //assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(response.Headers.Location.ToString(), $"/users/{createUser.Email}");
        }

        [Fact]
        public async Task given_unique_userId_user_should_be_remove()
        {
            var loginCredentials = LoginUser("test1@email.com", "secret");
            var payloadLogin = MakePayload(loginCredentials);
            var responseLogin = await Client.PostAsync("/users/account/login", payloadLogin);
            var responseLoginString = await responseLogin.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<JwtDto>(responseLoginString);
            GetAuthentication(token.Token);
            var user = await GetUserAsync("test1@email.com");
            var response = await Client.DeleteAsync($"/users/{user.Id}");
            //assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task given_unique_id_user_not_should_be_delete_without_authorize()
        {
            //arrange
            var userId = Guid.NewGuid();
            //act
            var response = await Client.DeleteAsync($"users/{userId}");
            //assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        private async Task<UserDto> GetUserAsync(string email)
        { 
            var response = await Client.GetAsync($"users/{email}");
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserDto>(responseString);
        }


        private CreateUser CreateNewUser(string email)
        {
            return new CreateUser
            {
                Email = email,
                Password = "secret",
                Username = "user",
                Fullname = "test",
                Role = "user"
            };
        }

        private Login LoginUser(string email, string password)
        {
            return new Login
            {
                Email = email,
                Password = password
            };
        }
        
    }
}
