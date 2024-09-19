using StockControl_EntityLayer.Entities.Abstract;
using StockControl_EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl_EntityLayer.Entities.Concrete
{
    public class Order:BaseEntity
    {
        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }
        [Display(Name ="Durum")]
       
        public Status Status { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
    }
}
