
//CreateMovieDto.cs (dữ liệu nhận vào khi tạo phim mới)
namespace MovieBooking.Application.Movies.DTOs;

public class CreateMovieDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public string Cast { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string AgeRating { get; set; } = "P";
    public string PosterUrl { get; set; } = string.Empty;
}