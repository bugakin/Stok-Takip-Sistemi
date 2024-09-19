using StockControl_EntityLayer.Entities.Concrete;
using StockControl_EntityLayer.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using StockControl_EntityLayer.Entities.Abstract;

namespace StockControl_EntityLayer
{
    public class User:BaseEntity
    {
        [DisplayName("Ad")]
        [Required(ErrorMessage = "Ad alanı boş geçilemez")]
        public string FirstName { get; set; }
        [DisplayName("Soyad")]
        [Required(ErrorMessage = "Soyad alanı boş geçilemez")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "E-mail boş geçilemez")]
        [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi formatı.")]
        public string Email { get; set; }
        [DisplayName("Adres")]
        [Required(ErrorMessage = "Adres boş geçilemez")]
        [StringLength(200, ErrorMessage = "En fazla 200 karakter girişi yapabilirsiniz")]
        public string Adress { get; set; }
        [Display(Name ="Parola")]
        [Required(ErrorMessage = "Parola boş geçilemez")]
        
        public string Password { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Photo { get; set; }
        public UserRole UserRole { get; set; }
        public ICollection<Order> Orders { get; set; }
        public User()
        {
            Orders = new HashSet<Order>();
        }
    }
}