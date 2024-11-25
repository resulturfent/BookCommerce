using DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Book_Commerce.Models
{
    public class UserViewModel : BaseViewModel
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyad zorunludur.")]
        [MinLength(2, ErrorMessage = "Tek kelimeli soyad olmaz")]
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; } 
        public string Email { get; set; }

        public List<BookViewModel>? BookViewModels { get; set; }
    }
}
