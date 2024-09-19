using StockControl_EntityLayer.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StockControl_EntityLayer
{
    public class Supplier:BaseEntity
    {
        [DisplayName("Tedarikçi Adı")]
        [Required(ErrorMessage = "Tedarikçi adı boş geçilemez")]
        public string SupplierName { get; set; }
        [DisplayName("Adres")]
        [Required(ErrorMessage = "Adres boş geçilemez")]
        [StringLength(200, ErrorMessage = "En fazla 200 karakter girişi yapabilirsiniz")]
        public string Adress { get; set; }
        [DisplayName("Telefon")]
        [Required(ErrorMessage = "Telefon boş geçilemez")]
        public string Phone { get; set; }
        
        [Required(ErrorMessage = "E-mail boş geçilemez")]
        [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi formatı.")]
        public string Email { get; set; }

        public ICollection<Product> Products { get; set; }
        public Supplier()
        {
            
            Products = new HashSet<Product>();
        }
    }
}