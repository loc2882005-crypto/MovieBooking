using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieBooking.Domain.Entities;
namespace MovieBooking.Application.Common.Interfaces
{

    //truy vấn dữ liệu mà không phụ thuộc trực tiếp vào tầng infrastructure
    public interface IApplicationDbContext
    {

        /*
         DbSet đại diện cho một bảng dữ liệu
         để get; nghĩa là tầng application chỉ được phép lấy bảng ra để thêm/sửa/xóa từng dòng dữ liệu,
         không được phép gán đè lại cả đối tượng
         */
        DbSet<Movie> Movies { get; }
        DbSet<Cinema> Cinemas { get; }
        DbSet<Room> Rooms { get; }
        DbSet<Seat> Seats { get; }
        DbSet<Showtime> Showtimes { get; }
        DbSet<Booking> Bookings { get; }
        DbSet<BookingDetail> BookingDetails { get; }

        /*
         SaveChangesAsync là lệnh thực thi bất đồng bộ (async/await) để đẩy toàn bộ thay đổi xuống SQL Server.
         Task<int>: trả về một số nguyên cho biết có bao nhiêu dòng dữ liệu vừa được cập nhật thành công dưới database
         CancellationToken : Cho phép ASP.NET Core tự động hủy tiến trình lưu database nếu người dùng đột ngột đóng trình duyệt
         */
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
