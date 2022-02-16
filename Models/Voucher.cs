using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FshopASP.Models
{
    public class Voucher
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Name { get; set; }
        public float Sale { get; set; }
        public DateTime StartDate { get; set;}
        public DateTime EndDate { get; set; }
        public int Limit { get; set; }
        public bool Status { get; set; }
       
    }
}
