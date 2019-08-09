using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using MiddlewareSample.Exceptions;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace MiddlewareSample.Middleware
{
    public class ErrorResponseMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionFeature?.Error;
            if (exception == null)
            {
                await _next(context);
            }
            var errorResponse = new MsRestApiErrorResponse();
            errorResponse.Error.Code = "InternalServerError";
            errorResponse.Error.Message = "An internal server error occurred.";
            var statusCode = HttpStatusCode.InternalServerError;
            if (exception is NotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                errorResponse.Error.Code = "NotFound";
                errorResponse.Error.Message = "The resource was not found";
            }
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }

        public class MsRestApiError
        {
            [JsonProperty("code")]
            public string Code { get; set; }
            [JsonProperty("message")]
            public string Message { get; set; }
        }
        public class MsRestApiErrorResponse
        {
            [JsonProperty("error")]
            public MsRestApiError Error { get; set; } = new MsRestApiError();
        }
    }
}
