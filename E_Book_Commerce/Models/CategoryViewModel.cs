using Newtonsoft.Json;

namespace Book_Commerce.Models
{
    public class CategoryViewModel : BaseViewModel
    {
                     //Primary Key
        public string CategoryName { get; set; }
        public List<BookViewModel> BookViewModels { get; set; }     //Bir kategoride birden fazla kitap olabilir.
        public List<BookCategoryViewModel> BookCategoryViewModels { get; set; }
    }
}
