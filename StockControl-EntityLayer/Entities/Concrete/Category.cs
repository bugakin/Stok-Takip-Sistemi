using StockControl_EntityLayer.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl_EntityLayer.Entities.Concrete
{
    public class Category:BaseEntity
    {
        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "Kategori adı boş geçilemez")]
        public string CategoryName { get; set; }

        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "Kategori adı boş geçilemez")]
        [StringLength(100, ErrorMessage ="En fazla 100 karakter girişi yapabilirsiniz")]
        public string Description  { get; set; }
        public ICollection<Product> Products { get; set; }
        public Category()
        {
            Products = new HashSet<Product>();//aynı eleman birden fazla kere eklenemesin diye hashset yaptık
        }
    }
}
