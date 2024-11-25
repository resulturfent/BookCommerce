using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AbstractService
{
    public interface ICartService
    {
        Task AddToCartAsync(int userId, int productId, int quantity); 
        Task RemoveFromCartAsync(int userId, int productId); 
        Task<CartDto> GetCartByUserIdAsync(int userId);
    }
}
