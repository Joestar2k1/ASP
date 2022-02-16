using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FshopASP.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string Code { get; set; }
        //foreign key
        public int AccountId { get; set; }
        public Account Account { get; set; }
        //
        public DateTime OrderDate { get; set; }
        public bool IsPaid { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingPhone { get; set; }
        public string ShippingName { get; set; }
        public int Total { get; set; }
        public bool Status { get; set; }
        public List<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
