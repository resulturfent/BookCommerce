using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class CategoryDto : BaseDto
    {
        [JsonIgnore]
        public int CategoryId { get; set; }                 //Primary Key
        public string CategoryName { get; set; }
        public List<BookDto> BookDtos { get; set; }     //Bir kategoride birden fazla kitap olabilir.
        public List<BookCategoryDto> BookCategoryDtos { get; set; }
    }
}
