using NetCore_OnionArchitecture.WebApi.Middlewares;

namespace NetCore_OnionArchitecture.WebApi.Extensions
{
    public static class AppExtensions
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app) 
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
       
        }
    }
}


