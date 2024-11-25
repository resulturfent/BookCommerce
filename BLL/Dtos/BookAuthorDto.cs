using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class BookAuthorDto : BaseDto
    {
        public int BookId { get; set; }
        public BookDto BookDto { get; set; }
        public int AuthorId { get; set; }
        public AuthorDto AuthorDto { get; set; }
    }
}
