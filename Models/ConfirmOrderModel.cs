using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlowersShop.Models
{
    public class ConfirmOrderModel
    {
        //Tổng tiền
        public double totalMoney { get; set; }
        //Người mua
        public string customerName { get; set; }
        [Required(ErrorMessage ="Chưa nhập sdt kìa man")]
        public string customerPhone { get; set; }
        public string customerEmail { get; set; }
        public string customerAddress { get; set; }
        public string paymentMethod { get; set; }
        //Người nhận
        public string customerNameConsume { get; set; }
        [Required]
        public string customerPhoneConsume { get; set; }
        [Required]
        public string customerAddressConsume { get; set; }
        [Required]
        public string customerProvince { get; set; }
        [Required]
        public string customerDistrict { get; set; }
        [Required]
        public string customerWard { get; set; }
        [Required]
        public string RecceiveTime { get; set; }

        //Mã giảm giá
        public string couponCode { get; set; }
        //Phương thức giao hàng
        public string ShippingMethod { get; set; }
    }
}