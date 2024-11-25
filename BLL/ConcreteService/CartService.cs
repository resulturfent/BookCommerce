using AutoMapper;
using BLL.AbstractService;
using BLL.Dtos;
using DAL.AbstractRepository;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ConcreteService
{
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly IMapper _mapper;
        public CartService(IRepository<Cart> cartRepository, IRepository<CartItem> cartItemRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }
        public async Task AddToCartAsync(int userId, int productId, int quantity) 
        {
            var carts = await _cartRepository.GetAllAsync(); 
            var cart = carts.FirstOrDefault(c => c.UserId == userId);
            if (cart == null) 
            { 
                cart = new Cart { UserId = userId, CartItems = new List<CartItem>() }; 
                await _cartRepository.AddAsync(cart); 
            } 
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId); 
            if (cartItem == null) 
            { 
                cartItem = new CartItem { ProductId = productId, Quantity = quantity, CartId = cart.Id }; cart.CartItems.Add(cartItem); 
                await _cartItemRepository.AddAsync(cartItem); 
            } 
            else 
            { 
                cartItem.Quantity += quantity; await _cartItemRepository.UpdateAsync(cartItem);
            } 
        }
        public async Task RemoveFromCartAsync(int userId, int productId)
        {
            var carts = await _cartRepository.GetAllAsync(); 
            var cart = carts.FirstOrDefault(c => c.UserId == userId); 
            if (cart != null) 
            { 
                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId); 
                if (cartItem != null) 
                { 
                    cart.CartItems.Remove(cartItem); 
                    await _cartItemRepository.DeleteAsync(cartItem.Id);
                }
            }
        }
        public async Task<CartDto> GetCartByUserIdAsync(int userId)
        {
            var carts = await _cartRepository.GetAllAsync(); 
            var cart = carts.FirstOrDefault(c => c.UserId == userId);
            if (cart == null)
            {
            return null; 
            }
                
            return _mapper.Map<CartDto>(cart);
        }
    } 
}
