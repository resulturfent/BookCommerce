using AutoMapper;
using BLL.AbstractService;
using Book_Commerce.Models;
using DAL.Data;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace Book_Commerce.Controllers
{
	public class CartController : Controller
	{
		private readonly IBookService _bookService;
		private readonly IMapper _mapper;
		private readonly ICartService _cartService;
		//private readonly ICategoryService _categoryService;
		//private readonly IAuthorService _authorService;
		private readonly AppDbContext _context;


		public CartController(
			IBookService bookService, IMapper mapper,
			//ICategoryService categoryService, 
			//IAuthorService authorService, 
			ICartService cartService,
			AppDbContext context)
		{
			_bookService = bookService;
			_cartService = cartService;
			_mapper = mapper;
			//_categoryService = categoryService;
			//_authorService = authorService;
			_context = context;
		}
		public IActionResult CartIndex()
		{
			var getBookId = HttpContext.Session.GetInt32("cartBookId");


			return View();
		}

		public IActionResult AddCart(int bookId)
		{
			if (bookId > 0)
			{
				//session'a eklenecek bookId
				//HttpContext.Session.SetInt32("cartBookId", bookId);
				//2,3,15=>
				var getUserId = HttpContext.Session.GetInt32("UserId");

				//int getUserId =Convert.ToInt32( HttpContext.Session.GetInt32("UserId"));
				var getBook = _bookService.GetBookById(bookId);

				if (getBook != null)
				{

					var getBookPrice = _bookService.GetBookPriceById(bookId);
					_cartService.AddToCartAsync(Convert.ToInt32( getUserId), bookId, 1);
					return Json(getBookPrice);
				}

			}
			return View();
		}
	}

}