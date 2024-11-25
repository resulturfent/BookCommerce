using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Author : BaseEntity
    {
        //  public int AuthorId { get; set; }               //Primary Key
        [Required(ErrorMessage = "Author full name is required.")]
        [MaxLength(50, ErrorMessage = ("Max 50 Characters allowed."))]
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public List<Book> Books { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
    }
}
