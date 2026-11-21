using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBooking.Domain.Entities;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;         // VD: Phòng chiếu 01, IMAX Room
    public int TotalRows { get; set; }                       // Số hàng ghế (VD: 10 -> A..J)
    public int TotalColumns { get; set; }                    // Số ghế mỗi hàng (VD: 12 -> 1..12)

    // Foreign Keys
    public int CinemaId { get; set; }

    // Navigation Properties
    public Cinema Cinema { get; set; } = null!;
    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    public ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
}