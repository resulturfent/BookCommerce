using BLL.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Book_Commerce.Models
{
    public class BookViewModel : BaseViewModel
    {
        //  public int bookId { get; set; }
        // Data Annotation. EFCORe'da veri doğrulama, veriyi bizim istediğimiz şekilde kısıtlama ve yönetme ihtiyacımı karşılayan özelliktir.
        //Data Annotation ilgili olduğu property'nin üstüne yazılır. ÇOK ÖNEMLİ !!!!!!!!!
        [MaxLength(100, ErrorMessage = "Çok fazla karakter girdiniz.")]
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Photo { get; set; }
        public IFormFile PhotoUrl { get; set; }
        public string PublishingHouse { get; set; }
        public string ISBN { get; set; }
        public AuthorDto AuthorDto { get; set; }          //Navigation Property  
        public string AuthorName { get; set; }
        //   public int AuthorId { get; set; }           //Foreign Key
        public CategoryDto CategoryDto { get; set; }      //Navigation Property
                                                          // public int CategoryId { get; set; }         //Foreign Key
        public string CategoryName { get; set; }
        public DateTime PublishedDate { get; set; }
        [Range(0, 15000.00)]
        public decimal UnitPrice { get; set; }
        public int UnitInStocks { get; set; }
        public UserViewModel UserViewModel { get; set; }
        public int UserId { get; set; }
        //yazar kitap ilişkisi için tanılanan iki kod aşağıdakiler.

        public List<BookAuthorViewModel> BookAuthorViewModels { get; set; }
        public List<BookCategoryViewModel> BookCategoryViewModels { get; set; }

        //public List<ShoppingCartViewModel> ShoppingCartViewModels { get; set; }
    }
}
