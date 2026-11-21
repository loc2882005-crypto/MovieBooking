using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MovieBooking.Domain.Enums;

namespace MovieBooking.Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public string BookingCode { get; set; } = string.Empty;  // VD: BK-20261117-8A9F (dùng tạo mã QR)
    public string UserId { get; set; } = string.Empty;       // Subject ID từ IdentityServer4
    public DateTime BookingDate { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Confirmed;

    // Foreign Keys
    public int ShowtimeId { get; set; }

    // Navigation Properties
    public Showtime Showtime { get; set; } = null!;
    public ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
}