using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBooking.Domain.Entities;

public class Movie
{
    public int Id { get; set; }
    /*
     string.Empty là 1 hằng số có sẵn,mang giá trị là một chuỗi rỗng ("")
     Giúp code rõ ràng hơn
     */
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;        // Hành động, Kinh dị, Tâm lý...
    public string Director { get; set; } = string.Empty;
    public string Cast { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string AgeRating { get; set; } = "P";             // P, T13, T16, T18
    public string PosterUrl { get; set; } = string.Empty;
    public string? TrailerUrl { get; set; }
    public bool IsActive { get; set; } = true;               // Ngừng chiếu thay vì xóa cứng

    /* Navigation Properties
      ICollection<T> là một interface, đại diện cho tập hợp có thể thêm,xóa,đếm và duyệt vòng lặp
      một Interface không thê tự khởi tạo bằng new Icollection nên phải dùng class thực thể 
      là new List đế cấp phát vùng bộ nhớ
     */
    public ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
}