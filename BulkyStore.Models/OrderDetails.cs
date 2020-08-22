using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BulkyStore.Models
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        
        public int OrderId { get; set; }
        
        [ForeignKey("OrderId")]
        public OrderHeader Orderheader { get; set; }
        
        public int ProductId { get; set; }
        
        [ForeignKey("ProductId")]
        public ProductModel Product { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }



    }
}
