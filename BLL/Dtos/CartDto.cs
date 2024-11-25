using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
   public class CartDto : BaseDto
    {
        //public int Id { get; set; }
        public int UserId { get; set; }
        public List<CartItemDto> CartItemDtos { get; set; }
    }
}
