using System.Net;

namespace BestFit.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger logger;
        private readonly RequestDelegate next
            ;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger,RequestDelegate next)
        {
            this.logger = logger;
            this.next= next;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();


                //Log the exception
                logger.LogError(ex,$"{errorId} : {ex.Message}");

                //Return A Custom Error Response
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Error: Something Went Wrong.Please contact your system administrator"
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
