using Microsoft.EntityFrameworkCore;
using MovieBooking.Application.Common.Interfaces;
using MovieBooking.Domain.Entities;
using MovieBooking.Domain.Enums;

namespace MovieBooking.Infrastructure.Persistence;

/*
 ApplicationDbContext là cầu nối giữa C# và SQL
 3 nhiệm vụ chính : ánh xạ 7 class, thiết lập ràng buộc bảo vệ dữ liệu 
 và nạp sẵn dữ liệu

 */

    // kế thừa DbContext để truy vấn SQL, thực thi IApplicationDbContext
public class ApplicationDbContext : DbContext, IApplicationDbContext
{

    //hàm khởi tạo(constructor) để DI truyền chuỗi kết nối SQL vào cho EF lúc khởi động
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // => Set<T>() tương tự {get;set;} giúp VS không cảnh báo khi bị null
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Cinema> Cinemas => Set<Cinema>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Seat> Seats => Set<Seat>();
    public DbSet<Showtime> Showtimes => Set<Showtime>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<BookingDetail> BookingDetails => Set<BookingDetail>();


    //thiết lập dữ liệu trong OnModelCreating(Fluent API)
    /*
        Fluent API là cơ chế cấu hình ánh xạ dữ liệu(mapping) bằng C# bên trong OnModelCreating của lớp 
        DbContext cho phép quy định chi tiết cách EF tạo bảng,kiểu cột,khóa ngoại và ràng buộc dưới SQL
     */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Định dạng kiểu tiền tệ decimal(18,2
        // Property : chọn thuộc tính, HascolumnType: quy định kiểu dữ liệu SQL
        modelBuilder.Entity<Showtime>().Property(s => s.BasePrice).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Booking>().Property(b => b.TotalAmount).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<BookingDetail>().Property(bd => bd.Price).HasColumnType("decimal(18,2)");

        // 2. Các chỉ mục duy nhất (Unique Indexes)
        //HasIndex : tạo chỉ mục cho 1 hoặc nhiều bảng
        modelBuilder.Entity<Room>()
            .HasIndex(r => new { r.CinemaId, r.Name })
            .IsUnique();

        modelBuilder.Entity<Seat>()
            .HasIndex(s => new { s.RoomId, s.RowLabel, s.ColumnNumber })
            .IsUnique();

        //đảm bảo không bao giờ trùng mã vé QR
        modelBuilder.Entity<Booking>()
            .HasIndex(b => b.BookingCode)
            .IsUnique();

        // Chốt chặn vật lý ở SQL Server: không thể tồn tại 2 vé trùng ghế trong cùng 1 suất chiếu
        modelBuilder.Entity<BookingDetail>()
            .HasIndex(bd => new { bd.ShowtimeId, bd.SeatId })
            .IsUnique();

        // 3. Cấu hình DeleteBehavior.Restrict tránh lỗi Multiple Cascade Paths của SQL Server
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;//gán quy tắc chặn xóa cho từng khóa ngoại tìm được
        }

        // 4. Data Seeding (Dữ liệu mẫu để test ngay ở Giai đoạn 2)
        //SeedData(modelBuilder);
    }


            //nạp dữ liệu tự động

    /*private static void SeedData(ModelBuilder modelBuilder)
    {
        //biến dữ liệu thành INSERT INTO.. nằm sẵn trong file Migration
        //hasData : Khai báo dữ liệu mẫu muốn thêm vào bảng
        modelBuilder.Entity<Cinema>().HasData(
            new Cinema { Id = 1, Name = "CGV Vincom Center", Address = "191 Bà Triệu, Hai Bà Trưng", City = "Hà Nội", Hotline = "19006017" }
        );

        modelBuilder.Entity<Room>().HasData(
            new Room { Id = 1, CinemaId = 1, Name = "Phòng chiếu 01", TotalRows = 5, TotalColumns = 6 }
        );

        // Tạo tự động 30 ghế (Hàng A-C: Standard, Hàng D-E: VIP)
        var seats = new List<Seat>();
        string[] rows = { "A", "B", "C", "D", "E" };
        int seatId = 1;
        for (int r = 0; r < rows.Length; r++)//tự động sinh 5 hàng từ A đến E
        {
            for (int c = 1; c <= 6; c++)//tự động sinh cột từ 1 đến 6
            {
                seats.Add(new Seat
                {
                    Id = seatId++,
                    RoomId = 1,
                    RowLabel = rows[r],
                    ColumnNumber = c,
                    SeatCode = $"{rows[r]}{c}",
                    SeatType = r >= 3 ? SeatType.VIP : SeatType.Standard,//phân loại ghế
                    //0,1,2 ghế thường, 3,4 ghế VIP
                    IsActive = true
                });
            }
        }
        modelBuilder.Entity<Seat>().HasData(seats);

        modelBuilder.Entity<Movie>().HasData(
            new Movie
            {
                Id = 1,
                Title = "Dune: Hành Tinh Cát - Phần Hai",
                Description = "Paul Atreides hợp nhất với người Fremen trên hành trình trả thù những kẻ đã hủy hoại gia đình anh.",
                Genre = "Khoa học viễn tưởng, Hành động",
                Director = "Denis Villeneuve",
                Cast = "Timothée Chalamet, Zendaya",
                DurationMinutes = 166,
                //năm 2026, tháng 11, ngày 1, giờ 00, phút 00, giây 00, xác định thời gian theo múi giờ UTC
                ReleaseDate = new DateTime(2026, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                AgeRating = "T13",
                PosterUrl = "https://image.tmdb.org/t/p/w500/dune2.jpg",
                IsActive = true
            },
            new Movie
            {
                Id = 2,
                Title = "Spider-Man: Du Hành Vũ Trụ Nhện",
                Description = "Miles Morales du hành qua đa vũ trụ và gặp gỡ biệt đội Người Nhện bảo vệ sự tồn vong của các thực tại.",
                Genre = "Hoạt hình, Hành động",
                Director = "Joaquim Dos Santos",
                Cast = "Shameik Moore, Hailee Steinfeld",
                DurationMinutes = 140,
                ReleaseDate = new DateTime(2026, 11, 10, 0, 0, 0, DateTimeKind.Utc),
                AgeRating = "P",
                PosterUrl = "https://image.tmdb.org/t/p/w500/spiderverse.jpg",
                IsActive = true
            }
        );

        modelBuilder.Entity<Showtime>().HasData(
            new Showtime
            {
                Id = 1,
                MovieId = 1,
                RoomId = 1,
                StartTime = new DateTime(2026, 11, 25, 19, 0, 0, DateTimeKind.Utc),
                EndTime = new DateTime(2026, 11, 25, 22, 1, 0, DateTimeKind.Utc), // 166p + 15p dọn phòng
                Format = "2D",
                BasePrice = 90000
            },
            new Showtime
            {
                Id = 2,
                MovieId = 2,
                RoomId = 1,
                StartTime = new DateTime(2026, 11, 25, 22, 15, 0, DateTimeKind.Utc),
                EndTime = new DateTime(2026, 11, 26, 0, 50, 0, DateTimeKind.Utc),
                Format = "3D",
                BasePrice = 110000
            }
        );
    }*/
}