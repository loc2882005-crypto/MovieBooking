using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBooking.Domain.Entities;

public class Showtime
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }                    // = StartTime + Movie.DurationMinutes + 15 phút dọn phòng
    public string Format { get; set; } = "2D";               // 2D, 3D, IMAX
    public decimal BasePrice { get; set; }                   // Giá vé cơ bản cho ghế Standard

    // Foreign Keys
    public int MovieId { get; set; }
    public int RoomId { get; set; }

    // Navigation Properties
    public Movie Movie { get; set; } = null!;
    public Room Room { get; set; } = null!;
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
}
