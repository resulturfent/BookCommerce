using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public List<Book> Books { get; set; }     //Bir kategoride birden fazla kitap olabilir. 
        public List<BookCategory> BookCategories { get; set; }
    }
}
