namespace Book_Commerce.Models
{
    public class BookCategoryViewModel : BaseViewModel
    {
        public int BookId { get; set; }
        public BookViewModel BookViewModel { get; set; }
        public int CategoryId { get; set; }
        public CategoryViewModel CategoryViewModel { get; set; }
    }
}
