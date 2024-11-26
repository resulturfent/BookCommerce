using AutoMapper;
using BLL.AbstractService;
using Book_Commerce.Models;
using DAL.Data;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Book_Commerce.Controllers
{
    public class CartController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        //private readonly ICategoryService _categoryService;
        //private readonly IAuthorService _authorService;
        private readonly AppDbContext _context;

      
        public CartController(
            IBookService bookService, IMapper mapper,
            //ICategoryService categoryService, 
            //IAuthorService authorService, 
            AppDbContext context)
        {
            _bookService = bookService;
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
                HttpContext.Session.SetInt32("cartBookId", bookId);
                //2,3,15=>

                var getBookId = HttpContext.Session.GetInt32("cartBookId");
                if (getBookId != null)
                {
                    var getBookPrice = _bookService.GetBookPriceById(bookId);

                    return Json(getBookPrice);
                }

            }
            return View();
        }
    } 

}
