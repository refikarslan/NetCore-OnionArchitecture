using NetCore_OnionArchitecture.Application;
using NetCore_OnionArchitecture.Shared;
using NetCore_OnionArchitecture.Persistence;
using NetCore_OnionArchitecture.WebApi.Extensions;

namespace NetCore_OnionArchitecture.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
             
            builder.Services.AddSharedInfrastructure(builder.Configuration);
            builder.Services.AddPersistenceInfrastructure(builder.Configuration);
            builder.Services.AddApplicationLayer();
            builder.Services.AddControllers();
            builder.Services.AddApiVersioningExtension();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseErrorHandlingMiddleware();

            app.MapControllers();

            app.Run();
        }
    }
}
