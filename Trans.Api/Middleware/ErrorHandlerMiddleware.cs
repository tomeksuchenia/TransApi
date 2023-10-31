using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using Trans.Core.Domain;
using Trans.Infrastructure.Exceptions;

namespace Trans.Api.Middleware
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger) : base()
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await HandlerExceptionAsync(context, ex);
            }
        }
        private static Task HandlerExceptionAsync(HttpContext context, Exception ex)
        {
            var errorCode = "error";
            var statusCode = HttpStatusCode.BadRequest;
            var exceptionType = ex.GetType();

            switch(ex)
            {
                case Exception e when exceptionType == typeof(UnauthorizedAccessException):
                    statusCode = HttpStatusCode.Unauthorized;
                    break;

                case ServiceException e when exceptionType == typeof(ServiceException):

                    if(e.Code == ErrorCodesService.UserNotFound)
                    {
                        statusCode = HttpStatusCode.NotFound;
                        errorCode = e.Code;
                        break;
                    }
                    statusCode = HttpStatusCode.BadRequest;
                    errorCode = e.Code;
                    break;

                case DomainException e when exceptionType == typeof(DomainException):
                    statusCode = HttpStatusCode.BadRequest;
                    errorCode = e.Code;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            var response = new {code = errorCode, message = ex.Message };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(payload);
            

        }
    }
}
