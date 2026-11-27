using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieBooking.Application.Common.Interfaces;
using MovieBooking.Infrastructure.Persistence;

namespace MovieBooking.Infrastructure;


//file này đóng vai trò là bảng lắp ráp của tầng infrastructure
//giúp MovieBooking.WebApi sử dụng được database mà không cần biết chi tiết cấu hình bên trong
//DI là kỹ thuật đưa các dependency từ bên ngoài vào class thay vì để class tự tạo chúng
public static class DependencyInjection
{
    //Extension Method : thêm phương thức mới vào kiểu dữ liệu có sẵn mà không cần sửa mã nguồn
    //nhờ vậy trong file program.cs ở tầng WebApi, chỉ cần khai báo : 
    //  builder.Services.AddInfrastructureServices(builder.Configuration);

    //IServiceCollection : nơi đăng ký các dịch vụ
    //IConfiguration : đọc cấu hình ứng dụng
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //đăng ký dịch vụ ApplicationDbContext vào DI Container và cấu hình nó để sử dụng SQL Server làm hệ quản trị CSDL
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));//đọc file appsettings.json để lấy chuoxi kết nối tới SQL

        //AddScoped : tạo một đối tượng trong mỗi HTTP request
        //bất cứ khi nào Application yêu cầu IApplicationDbContext, hãy lấy đối tượng ApplicationDbContext đã đăng ký ở trên để đưa cho nó
        services.AddScoped<IApplicationDbContext>(
            provider => provider.GetRequiredService<ApplicationDbContext>());//đảm báo trong cùng một HTTP request, hệ thống luôn tái sử dụng đúng 1 đối tượng là ApplicationDbContext duy nhât

        return services;
    }
}