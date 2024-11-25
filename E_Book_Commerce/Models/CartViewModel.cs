using BLL.Dtos;

namespace Book_Commerce.Models
{
    public class CartViewModel : BaseViewModel
    {
        //public int Id { get; set; }
        public int UserId { get; set; }
        public List<CartItemDto> CartItemDtos { get; set; }
    }
}
