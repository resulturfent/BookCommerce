using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Book : BaseEntity
    {
      /* Data Annotation.EFCORe'da veri doğrulama, veriyi bizim istediğimiz şekilde kısıtlama ve yönetme ihtiyacımı karşılayan özelliktir.
        Data Annotation ilgili olduğu property'nin üstüne yazılır. ÇOK ÖNEMLİ !!!!!!!!!*/

       [MaxLength(100, ErrorMessage = "Çok fazla karakter girdiniz.")]
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Photo { get; set; }
        public string PublishingHouse { get; set; }
        public string ISBN { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

        public DateTime PublishedDate { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitInStocks { get; set; }
      //  public List<ShoppingCart> ShoppingCarts { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
        public List<BookCategory> BookCategories { get; set; }
    }
}
