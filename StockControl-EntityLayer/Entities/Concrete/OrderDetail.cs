using StockControl_EntityLayer.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl_EntityLayer.Entities.Concrete
{
    public class OrderDetail:BaseEntity
    {
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public Product Product { get; set; }
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        public Order Order { get; set; }

        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
