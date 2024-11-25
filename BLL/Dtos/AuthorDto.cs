using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class AuthorDto : BaseDto
    {
        //   public int AuthorId { get; set; }               //Primary Key
        [Required(ErrorMessage = "Author full name is required.")]
        [MaxLength(50, ErrorMessage = ("Max 50 Characters allowed."))]
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public List<BookDto> BookDtos { get; set; }
        public List<BookAuthorDto> BookAuthorDtos { get; set; }
    }
}
