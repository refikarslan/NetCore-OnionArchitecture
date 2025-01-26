using NetCore_OnionArchitecture.Application.Wrappers;
using System.Net;
using System.Text.Json;

namespace NetCore_OnionArchitecture.WebApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            { 
                await _next(context);
            }
            catch(Exception error) 
            {
                var response = context.Response;
                response.ContentType = "application/json";  
                var responseModel = new Response<string>() { Success = false, Message= "Bir hata oluştu. Lütfen tekrar deneyin." };

                // Hata koduna göre HTTP yanıt kodunu ayarla
                switch (error)
                {
                    case Application.Exceptions.ApiException e:
                        //custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case Application.Exceptions.ValidationException e:
                        //custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors;
                        break;

                    case KeyNotFoundException e:
                        //not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        //unhandler error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                // JSON yanıtı
                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }

        }
    }
}
