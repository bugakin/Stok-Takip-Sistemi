using StockControl_EntityLayer.Entities.Abstract;
using StockControl_EntityLayer.Entities.Concrete;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockControl_EntityLayer
{
    public class Product:BaseEntity
    {
        [DisplayName("Ürün Adı")]
        [Required(ErrorMessage ="Ürün adı boş geçilemez")]
        public string ProductName { get; set; }
        [DisplayName("Birim Fiyat")]
        [Required(ErrorMessage = "Birim fiyat boş geçilemez")]
        public double UnitPrice { get; set; }
        [DisplayName("Stok")]
        
        public int? Stock {  get; set; }
        [DisplayName("Son Kullanma tarihi")]
        public DateTime? ExpireDate { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public Category?  Category { get; set; }
        [ForeignKey("Supplier")]
        public int SupplierID { get; set; }
        public Supplier? Supplier { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

    }
}