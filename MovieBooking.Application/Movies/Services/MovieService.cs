using Microsoft.EntityFrameworkCore;//cung cấp phương thức như Where(),ToListAsync()
using MovieBooking.Application.Movies.Interfaces;//chứa Interface IMovieService
using MovieBooking.Application.Common.Interfaces;//chứa Interface IapplicationDbContext
using MovieBooking.Application.Movies.DTOs;//chứa các DTO
using MovieBooking.Domain.Entities;//chứa  Entity Movie

namespace MovieBooking.Application.Movies.Services;

//MovieService : nhiệm vụ xử lý các chức năng liên quan đến phim

public class MovieService : IMovieService
{

    
     /*DI-IApplicationDbContext*/
    private readonly IApplicationDbContext _context;//truy cập các bảng trong Database

    //constructor của MovieServices : nhận đối tượng đó thông qua DI
    public MovieService(IApplicationDbContext context)
    {
        _context = context;//gán đối tượng truyền vào để các phương thức bên trong MovieService có thể sử dụng
    }

    //lấy danh sách tất cả bộ phim đang hoạt động, sắp xếp theo ngày phát hành từ mới nhất đến cũ nhất
    //async : phương thức xử lý bất đồng bộ
    public async Task<List<MovieDto>> GetAllActiveMoviesAsync()
    {
        return await _context.Movies//truy cập dữ liệu Movies thông qua DbContext
            .Where(m => m.IsActive) //where() dùng để lọc dữ liệu theo điều kiện
            .OrderByDescending(m => m.ReleaseDate)
            .Select(m => new MovieDto  //chuyển Enity Movie thành MovieDto, select() tạo một đối tượng
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                Genre = m.Genre,
                Director = m.Director,
                Cast = m.Cast,
                DurationMinutes = m.DurationMinutes,
                ReleaseDate = m.ReleaseDate,
                AgeRating = m.AgeRating,
                PosterUrl = m.PosterUrl
            })
            .ToListAsync();//thực hiện truy vấn bất đồng bộ
    }

    //lấy thông tin phim theo ID
    //tham số id : là ID người dùng muốn tìm
    public async Task<MovieDto?> GetMovieByIdAsync(int id)
    {
        return await _context.Movies
            .Where(m => m.Id == id && m.IsActive)
            .Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                Genre = m.Genre,
                Director = m.Director,
                Cast = m.Cast,
                DurationMinutes = m.DurationMinutes,
                ReleaseDate = m.ReleaseDate,
                AgeRating = m.AgeRating,
                PosterUrl = m.PosterUrl
            })
            .FirstOrDefaultAsync();//trả về đối tượng tìm được hoặc null
    }

    //nhận thông tin phim mới, tạo Entity Movie, lưu vào DB và trả về thông tin vừa tạo
    //CreateMovieDto request : đối tượng chứa thông tin phim mới mà người dùng gửi lên
    //request : đối tượng chứa thông tin phim mới mà người dùng gửi lên
    public async Task<MovieDto> CreateMovieAsync(CreateMovieDto request)
    {
        //request là dữ liệu đầu vào từ người dùng, còn movie là đối tượng Entity chuẩn bị lưu vào DB
        var movie = new Movie
        {
            Title = request.Title,
            Description = request.Description,
            Genre = request.Genre,
            Director = request.Director,
            Cast = request.Cast,
            DurationMinutes = request.DurationMinutes,
            ReleaseDate = request.ReleaseDate,
            AgeRating = request.AgeRating,
            PosterUrl = request.PosterUrl,
            IsActive = true
        };

        _context.Movies.Add(movie);//thêm vào DbContext
        await _context.SaveChangesAsync();//lưu dữ liệu vào DB

        return new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            Genre = movie.Genre,
            Director = movie.Director,
            Cast = movie.Cast,
            DurationMinutes = movie.DurationMinutes,
            ReleaseDate = movie.ReleaseDate,
            AgeRating = movie.AgeRating,
            PosterUrl = movie.PosterUrl
        };
    }


}
