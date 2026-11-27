using MovieBooking.Application;
using MovieBooking.Infrastructure;

namespace MovieBooking.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            //đăng ký toàn bộ dịch vụ thuộc tầng Infrastructure vào hệ thống DI 
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
