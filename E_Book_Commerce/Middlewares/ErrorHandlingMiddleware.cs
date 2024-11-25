using Book_Commerce.LogService;
using Newtonsoft.Json;

namespace Book_Commerce.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IServiceScopeFactory scopeFactory)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                using (var scope = scopeFactory.CreateScope())
                {
                    var logService = scope.ServiceProvider.GetRequiredService<ILogService>();
                    int statusCode = context.Response.StatusCode == 200 ? 500 : context.Response.StatusCode;

                    var errorLog = await logService.LogErrorAsync(ex, statusCode);

                    context.Response.ContentType = "application/json";

                    var result = JsonConvert.SerializeObject(new
                    {
                        errorLog.Message,
                        errorLog.StatusCode,
                        errorLog.StackTrace,
                        errorLog.CreatedAt
                    });
                    await context.Response.WriteAsync(result);
                }
            }
        }
        //IServiceScopeFactory görüş alanı olarak düşünebiliriz. Kendimize görüş alanı oluşturuyoruz
    }
}

