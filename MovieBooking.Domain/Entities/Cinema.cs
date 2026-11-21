using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBooking.Domain.Entities
{
    public class Cinema
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;         // VD: CGV Vincom Bà Triệu
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;         // VD: Hà Nội, TP.HCM
        public string? Hotline { get; set; }

        // Navigation Properties
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
