using Microsoft.AspNetCore.Mvc;
using Trans.Core.Domain;
using Trans.Infrastructure.Dto;

namespace Trans.Api.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
      protected Guid UserId => User?.Identity?.IsAuthenticated == true ?
                    Guid.Parse(User.Identity.Name):
                    Guid.Empty;
    }
}


