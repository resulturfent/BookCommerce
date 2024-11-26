using AutoMapper;
using BLL.AbstractService;
using BLL.Dtos;
using Book_Commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Commerce.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.GetAllAuthors();
            var authorViewModel = _mapper.Map<List<AuthorViewModel>>(authors);

            return View(authorViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(List<int> authorIds)
        {
            //var allPosts = await _bookService.get(categoryIds);
            //var newAllPosts = _mapper.Map<List<BookViewModel>>(allPosts);

            var authors = await _authorService.GetAllAuthors();
            var allAuthors = _mapper.Map<List<AuthorViewModel>>(authors);

            ViewBag.Authors = allAuthors;
            return View(allAuthors);
        }

        public async Task<IActionResult> AuthorList()
        {
            var authors = await _authorService.GetAllAuthors();
            var authorViewModel = _mapper.Map<List<AuthorViewModel>>(authors);

            return View(authorViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AuthorList(List<int> authorIds)
        {
            //var allPosts = await _bookService.get(categoryIds);
            //var newAllPosts = _mapper.Map<List<BookViewModel>>(allPosts);

            var authors = await _authorService.GetAllAuthors();
            var allAuthors = _mapper.Map<List<AuthorViewModel>>(authors);

            ViewBag.Authors = allAuthors;
            return View(allAuthors);
        }
        public IActionResult AddAuthor()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAuthor(AuthorViewModel authorViewModel)
        {
            await _authorService.CreateAuthor(_mapper.Map<AuthorDto>(authorViewModel));
            return RedirectToAction("", "Author");
        }

		public IActionResult AddAuthorAdmin()
		{
			return View();
		}

        [HttpPost]
		public async Task<IActionResult> AddAuthorAdmin(AuthorViewModel authorViewModel)
		{
			await _authorService.CreateAuthor(_mapper.Map<AuthorDto>(authorViewModel));
			return RedirectToAction("AuthorList", "Author");
		}

		[HttpPost]
        public async Task<IActionResult> Delete(int authorId)
        {
            await _authorService.DeleteAuthor(authorId);
            return RedirectToAction("AuthorList", "Author");

        }

        [HttpGet]
        public async Task<IActionResult> UpdateAuthor(int authorId)
        {
            var authors = await _authorService.GetAllAuthors(); //authoru buluyoruz.
            foreach (var item in authors)
            {
                if (item.Id == authorId)
                {
                    return View(_mapper.Map<AuthorViewModel>(item));
                }
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> UpdateAuthor(int authorId, AuthorViewModel authorViewModel)
        {
            authorViewModel.Id = authorId;
            await _authorService.UpdateAuthor(_mapper.Map<AuthorDto>(authorViewModel));
            return RedirectToAction("AuthorList", "Author");
        }
    }
}
