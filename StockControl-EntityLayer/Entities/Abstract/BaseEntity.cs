using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl_EntityLayer.Entities.Abstract
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public DateTime AddedDate { get; set; }=DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
