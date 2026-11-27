

//IMovieService : quy định Service có những chức năng gì 
using MovieBooking.Application.Movies.DTOs;//sử dụng lớp thuộc namspace mà không cần viết đầy đủ tên
namespace MovieBooking.Application.Movies.Interfaces
{
    public interface IMovieService
    {

        //khai báo phương thức bất đồng bộ có tên là GetAllActiveMoviesAsync, thực hiện việc lấy danh sách phim và trả về kết quả kiểu List
        Task<List<MovieDto>> GetAllActiveMoviesAsync();
        Task<MovieDto?> GetMovieByIdAsync(int id);
        Task<MovieDto> CreateMovieAsync(CreateMovieDto request);
    }
}
