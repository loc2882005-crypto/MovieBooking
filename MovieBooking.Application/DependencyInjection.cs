using Microsoft.Extensions.DependencyInjection;
using MovieBooking.Application.Movies.Interfaces;
using MovieBooking.Application.Movies.Services;

namespace MovieBooking.Application;

//dependency Injection : đăng ký các Services của tầng Application vào DI Container, để những 
//thành phần khác có thể sử dụng mà không cần tạo đối tượng new
public static class DependencyInjection
{
    /*
     AddApplicationServices : phương thức mở rộng để đăng ký Services
     Program.cs: Gọi AddApplicationServices() để đưa các đăng ký vào DI Container
     */
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //khi có thành phần yêu cầu IMovieService, DI sẽ cung cấp một đối tượng MovieService để xử lý
        services.AddScoped<IMovieService, MovieService>();
        return services;
    }
}