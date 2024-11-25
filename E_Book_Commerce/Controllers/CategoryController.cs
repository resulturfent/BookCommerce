using AutoMapper;
using BLL.AbstractService;
using BLL.Dtos;
using Book_Commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Commerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoryController(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategories();
            var categoryViewModels = _mapper.Map<List<CategoryViewModel>>(categories);

            return View(categoryViewModels);
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
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewModel categoryViewModel)
        {
            await _categoryService.CreateCategory(_mapper.Map<CategoryDto>(categoryViewModel));
            return RedirectToAction("Index", "Category");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int categoryId)
        {
            await _categoryService.DeleteCategory(categoryId);
            return RedirectToAction("Index", "Category");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int categoryId)
        {
            var categories = await _categoryService.GetAllCategories(); //kategoriyi buluyoruz.
            foreach (var item in categories)
            {
                if (item.Id == categoryId)
                {
                    return View(_mapper.Map<CategoryViewModel>(item));
                }
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(int categoryId, CategoryViewModel categoryViewModel)
        {
            categoryViewModel.Id = categoryId;
            await _categoryService.UpdateCategory(_mapper.Map<CategoryDto>(categoryViewModel));
            return RedirectToAction("Index", "Category");
        }
    }
}
