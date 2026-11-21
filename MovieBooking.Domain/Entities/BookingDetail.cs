using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBooking.Domain.Entities;

public class BookingDetail
{
    public int Id { get; set; }
    public decimal Price { get; set; }                       // Lưu chốt giá thực tế tại thời điểm mua

    // Foreign Keys
    public int BookingId { get; set; }
    public int ShowtimeId { get; set; }                      // Dùng để đánh Unique Index (ShowtimeId, SeatId)
    public int SeatId { get; set; }

    // Navigation Properties
    public Booking Booking { get; set; } = null!;
    public Showtime Showtime { get; set; } = null!;
    public Seat Seat { get; set; } = null!;
}
