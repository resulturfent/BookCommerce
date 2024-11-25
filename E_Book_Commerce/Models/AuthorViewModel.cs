using System.ComponentModel.DataAnnotations;

namespace Book_Commerce.Models
{
    public class AuthorViewModel : BaseViewModel
    {
        // public int AuthorId { get; set; }               //Primary Key
        [Required(ErrorMessage = "Author full name is required.")]
        [MaxLength(50, ErrorMessage = ("Max 50 Characters allowed."))]
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public List<BookViewModel> BookViewModels { get; set; }
        public List<BookAuthorViewModel> BookAuthorViewModels { get; set; }
    }
}
