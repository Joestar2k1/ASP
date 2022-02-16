using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FshopASP.Models
{
    public class Cart
    {  
        public int Id { get; set; }
        //foreign key
        public int AccountId { get; set; }
        public Account  Account { get; set; }
        //
        //foreign key
        public int ProductId { get; set; }
        public Product Product { get; set; }
        //end
        public int Quantity { get; set; }
    }
}
