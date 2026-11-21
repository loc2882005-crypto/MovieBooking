using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBooking.Domain.Enums
{

    //trạng thái đơn đặt vé
    public enum BookingStatus
    {
        Pending = 1,//đang giữ ghế chờ thanh toán
        Confirmed = 2, //Đã thanh toán thành công
        Cancelled = 3, //Đã hủy/hết hạn giữ ghế
    }
}
