using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace FshopASP.Models
{
    public class Product
    {
        [DisplayName("STT")]
        public int Id { get; set; }
     
        [DisplayName("Mã sản phẩm")]
        public string Code { get; set; }
 
        [DisplayName("Tên sản phẩm")]
        public string Name { get; set; }

        [DisplayName("Loại sản phẩm")]
        public string Type { get; set; }

        [DisplayName("Giá bán")]
        public int Price { get; set; } 

        [DisplayName("Ảnh minh họa")]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [DisplayName("Số lượng")]
        public int Quantity { get; set; }

        [DisplayName("Mô tả")]
     
        public string Description { get; set; }
        [DisplayName("Đơn vị tính")]
        public string Unit { get; set; }

        [DisplayName("Trạng thái")]
        public bool Status { get; set; }
        public List<InvoiceDetail> InvoiceDetails { get; set; }
        public List<Cart> Carts { get; set; }
        public List<Voucher> Vouchers { get; set; }
        public List<AccountReview> AccountReviews { get; set; }

    }
}
