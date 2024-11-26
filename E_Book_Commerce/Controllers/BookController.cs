using AutoMapper;
using BLL.AbstractService;
using BLL.Dtos;
using Book_Commerce.Models;
using DAL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Book_Commerce.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IAuthorService _authorService;
        private readonly AppDbContext _context;

        #region Book Kullanıcı işlemleri
        public BookController(IBookService bookService, IMapper mapper, ICategoryService categoryService, IAuthorService authorService, AppDbContext context)
        {
            _bookService = bookService;
            _mapper = mapper;
            _categoryService = categoryService;
            _authorService = authorService;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooks();
            ViewBag.Categories = await _categoryService.GetAllCategories();
            var bookViewModel = _mapper.Map<List<BookViewModel>>(books);

            if (books == null || !books.Any())
            {
                // Eğer liste boşsa, kullanıcıya bilgi ver.
                ViewBag.Message = "Şu anda görüntülenecek bir kitap bulunmamaktadır.";
                return View(new List<BookViewModel>()); // Boş bir liste gönder
            }

            return View(bookViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> BookList()
        {
            var books = await _bookService.GetAllBooks();
            ViewBag.Categories = await _categoryService.GetAllCategories();
            var bookViewModel = _mapper.Map<List<BookViewModel>>(books);

            if (books == null || !books.Any())
            {
                // Eğer liste boşsa, kullanıcıya bilgi ver.
                ViewBag.Message = "Şu anda görüntülenecek bir kitap bulunmamaktadır.";
                return View(new List<BookViewModel>()); // Boş bir liste gönder
            }

            return View(bookViewModel);
        }



        [HttpPost]
        public async Task<IActionResult> Index(List<int> categoryIds)
        {
            //var allPosts = await _bookService.get(categoryIds);
            //var newAllPosts = _mapper.Map<List<BookViewModel>>(allPosts);

            var categories = await _categoryService.GetAllCategories();
            var allCategories = _mapper.Map<List<CategoryViewModel>>(categories);

            ViewBag.Categories = allCategories;
            return View(allCategories);
        }
        [HttpGet]
        public async Task<IActionResult> AddBook()
        {
            var allAuthors = await _authorService.GetAllAuthors();
            ViewBag.Authors = allAuthors;

            // AuthorDto listesini SelectListItem listesine dönüştürün
            ViewBag.AuthorList = allAuthors.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(), // AuthorDto içinde Id alanını kullanın
                Text = $"{a.AuthorName} {a.AuthorSurname}"      // Ad ve Soyad birleştiriliyor

            }).ToList();
            var allCategories = await _categoryService.GetAllCategories();
            ViewBag.Categories = allCategories;
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> AddBook(BookViewModel bookViewModel, List<int> authorIds, List<int> categoryIds)
        {
            if (bookViewModel.PhotoUrl != null)
            {
                var fileName = Path.GetFileName(bookViewModel.PhotoUrl.FileName);
                var filePath = Path.Combine("wwwroot", "Images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await bookViewModel.PhotoUrl.CopyToAsync(stream);
                }
                bookViewModel.Photo = fileName;
            }
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                ViewBag.Hata = "Kullanıcı girişi yapılmamış. Kitap eklemek için giriş yapınız.";
                return View(bookViewModel);
            }

            // BookDto'ya UserId'yi ekleyin
            var bookDto = _mapper.Map<BookDto>(bookViewModel);
            bookDto.UserId = userId.Value;

            await _bookService.CreateBook(bookDto);

            // await _bookService.CreateBook(_mapper.Map<BookDto>(bookViewModel)); 
            //yukarıdaki kod ile book view model bookdto ya map lıyoruz. ardından book service teki create booka direkt atama yapıyoruz. ancak bu kod her nedense çalışmadı. daha yukarıdaki BookDto ya UserId ekleyin kısmını chat gpt den aldım ve uyguladım. burada UserId li kitap ekleme işlemini yapabildim. 

            var newBook = await _bookService.GetAllBooks();
            var newestBook = newBook.OrderByDescending(x => x.Id).FirstOrDefault();
            foreach (var item in authorIds)
            {

                await _bookService.AddBookAuthor(new BookAuthorDto { AuthorId = item, BookId = newestBook.Id });
            }
            foreach (var item in categoryIds)
            {
                await _bookService.AddBookCategory(new BookCategoryDto { CategoryId = item, BookId = newestBook.Id });
            }
            return RedirectToAction("Index", "Book");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteBook(int bookId)
        {

            await _bookService.DeleteBook(bookId);
            return RedirectToAction("BookListAdmin", "Book");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBook(int Id)
        {
            try
            {
                var books = _bookService.GetBookById(Id);

                if (books != null)
                {
                    var result = _mapper.Map<BookDto>(books);

                    return View(result);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("BookListAdmin", "Book");

            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBookAdmin(int Id)
        {
            try
            {
                var books = _bookService.GetBookById(Id);

                if (books != null)
                {
                    var result = _mapper.Map<BookDto>(books);

                    return View(result);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("BookListAdmin", "Book");

            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateBook(int bookUpdateId, BookViewModel bookViewModel)
        {

            bookViewModel.Id = bookUpdateId;
            await _bookService.UpdateBook(_mapper.Map<BookDto>(bookViewModel));
            return RedirectToAction("UpdateBook", "Book");
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks() ?? new List<BookDto>();
            var bookViewModel = _mapper.Map<List<BookViewModel>>(books);
            // ViewBag.Categories = await _categoryService.GetAllCategories();
            return View(bookViewModel);
        }
        #endregion

        #region Book Admin işlemleri

        [HttpGet]
        public async Task<IActionResult> BookListAdmin()
        {
            var books = await _bookService.GetAllBooks();
            ViewBag.Categories = await _categoryService.GetAllCategories();
            var bookViewModel = _mapper.Map<List<BookViewModel>>(books);

            if (books == null || !books.Any())
            {
                // Eğer liste boşsa, kullanıcıya bilgi ver.
                ViewBag.Message = "Şu anda görüntülenecek bir kitap bulunmamaktadır.";
                return View(new List<BookViewModel>()); // Boş bir liste gönder
            }

            return View(bookViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddBookAdmin()
        {
            var allAuthors = await _authorService.GetAllAuthors();
            ViewBag.Authors = allAuthors;

            // AuthorDto listesini SelectListItem listesine dönüştürün
            ViewBag.AuthorList = allAuthors.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(), // AuthorDto içinde Id alanını kullanın
                Text = $"{a.AuthorName} {a.AuthorSurname}"      // Ad ve Soyad birleştiriliyor

            }).ToList();
            var allCategories = await _categoryService.GetAllCategories();
            ViewBag.Categories = allCategories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBookAdmin(BookViewModel bookViewModel, List<int> authorIds, List<int> categoryIds)
        {
            if (bookViewModel.PhotoUrl != null)
            {
                var fileName = Path.GetFileName(bookViewModel.PhotoUrl.FileName);
                var filePath = Path.Combine("wwwroot", "Images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await bookViewModel.PhotoUrl.CopyToAsync(stream);
                }
                bookViewModel.Photo = fileName;
            }
            var userId = HttpContext.Session.GetInt32("UserId");
            //var userId = 2;

            if (userId == null)
            {
                ViewBag.Hata = "Kullanıcı girişi yapılmamış. Kitap eklemek için giriş yapınız.";
                return View(bookViewModel);
            }

            // BookDto'ya UserId'yi ekleyin
            var bookDto = _mapper.Map<BookDto>(bookViewModel);
            bookDto.UserId = userId.Value;


            await _bookService.CreateBook(bookDto);

            // await _bookService.CreateBook(_mapper.Map<BookDto>(bookViewModel)); 
            //yukarıdaki kod ile book view model bookdto ya map lıyoruz. ardından book service teki create booka direkt atama yapıyoruz. ancak bu kod her nedense çalışmadı. daha yukarıdaki BookDto ya UserId ekleyin kısmını chat gpt den aldım ve uyguladım. burada UserId li kitap ekleme işlemini yapabildim. 

            var newBook = await _bookService.GetAllBooks();
            var newestBook = newBook.OrderByDescending(x => x.Id).FirstOrDefault();
            foreach (var item in authorIds)
            {

                await _bookService.AddBookAuthor(new BookAuthorDto { AuthorId = item, BookId = newestBook.Id });
            }
            foreach (var item in categoryIds)
            {
                await _bookService.AddBookCategory(new BookCategoryDto { CategoryId = item, BookId = newestBook.Id });
            }
            return RedirectToAction("BookListAdmin", "Book");
        }



        #endregion
    }
}
