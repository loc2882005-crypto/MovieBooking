using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBooking.Domain.Enums;


//phân loại ghế và hệ số/giá phụ thu
public enum SeatType
{
    Standard = 1, // Ghế thường (giá bằng BasePrice của suất chiếu)
    VIP = 2,      // Ghế VIP (cộng thêm phụ thu, ví dụ +20.000đ)
    Couple = 3    // Ghế đôi
}