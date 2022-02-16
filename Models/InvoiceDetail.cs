using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FshopASP.Models
{
    public class InvoiceDetail
    {
        public int Id { get; set; }

        //foreign key 
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        //end
        //foreign key 
        public int ProductId { get; set; }
        public Product Product { get; set; }
        //end
        public int Quantity { get; set; }

        public int Total { get; set; }
    }
}
