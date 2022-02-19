using StudentSpy.DataManager.Helpers;
using System.Net;
using System.Text.Json;

namespace StudentSpy.Web.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlerMiddleware> logger;

        public ErrorHandlerMiddleware(RequestDelegate nxt,
            ILogger<ErrorHandlerMiddleware> log)
        {
            next = nxt;
            logger = log;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        logger.LogError(e.Message);
                        break;
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        logger.LogError(e.Message);
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        logger.LogError("500. Internal Server Error");
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
