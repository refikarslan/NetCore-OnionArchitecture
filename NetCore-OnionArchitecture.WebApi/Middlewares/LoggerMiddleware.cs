namespace NetCore_OnionArchitecture.WebApi.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggerMiddleware> _logger;

        public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // İstekten önce loglama
            _logger.LogInformation("Handling request: {Method} {Url}", context.Request.Method, context.Request.Path);

            // Bir sonraki middleware'i çağır
            await _next(context);

            // İstekten sonra loglama
            _logger.LogInformation("Finished handling request: {StatusCode}", context.Response.StatusCode);
        }
    }
}
