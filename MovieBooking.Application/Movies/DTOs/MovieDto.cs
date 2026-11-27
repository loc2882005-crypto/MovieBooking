namespace MovieBooking.Application.Movies.DTOs;

/*
    DTO(Data Transfer Object) là các class trung gian dùng để định hình dữ liệu nhận vào và trả cho API
    giúp che giấu các trường nội bộ như IsActive của Entity gốc

    MovieDto.cs là file trả về cho client
 */
public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public string Cast { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string AgeRating { get; set; } = string.Empty;
    public string PosterUrl { get; set; } = string.Empty;
}