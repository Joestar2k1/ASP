using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FshopASP.Models;

namespace FshopASP.Models
{
    public class AccountReview
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public string Content { get; set; }
        public int Quantity { get; set; }
        public DateTime PostedDate { get; set; }
    }
}
