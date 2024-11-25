using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class BookCategoryDto : BaseDto
    {
        public int BookId { get; set; }
        public BookDto BookDto { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto CategoryDto { get; set; }
    }
}
