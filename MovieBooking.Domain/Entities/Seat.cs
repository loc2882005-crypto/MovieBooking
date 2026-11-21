using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MovieBooking.Domain.Enums;

namespace MovieBooking.Domain.Entities;

public class Seat
{
    public int Id { get; set; }
    public string RowLabel { get; set; } = string.Empty;     // A, B, C...
    public int ColumnNumber { get; set; }                    // 1, 2, 3...
    public string SeatCode { get; set; } = string.Empty;     // A1, A2, F8...
    public SeatType SeatType { get; set; } = SeatType.Standard;
    public bool IsActive { get; set; } = true;               // Đánh dấu ghế hỏng/bảo trì nếu cần

    // Foreign Keys
    public int RoomId { get; set; }

    // Navigation Properties
    public Room Room { get; set; } = null!;
    public ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
}
