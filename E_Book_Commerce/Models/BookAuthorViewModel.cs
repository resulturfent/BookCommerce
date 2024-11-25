namespace Book_Commerce.Models
{
    public class BookAuthorViewModel : BaseViewModel
    {
        public int BookId { get; set; }
        public BookViewModel BookViewModel { get; set; }
        public int AuthorId { get; set; }
        public AuthorViewModel AuthorViewModel { get; set; }
    }
}
