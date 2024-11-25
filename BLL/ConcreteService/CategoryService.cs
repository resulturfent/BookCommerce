using AutoMapper;
using BLL.AbstractService;
using BLL.Dtos;
using DAL.AbstractRepository;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ConcreteService
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task CreateCategory(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.AddAsync(category);
        }

        public async Task DeleteCategory(int categoryId)
        {
            var categories = await _categoryRepository.GetByIdAsync(categoryId);
            if (categories != null)
            {
                await _categoryRepository.DeleteAsync(categoryId);
            }
        }

        public async Task<List<CategoryDto>> GetAllCategories()
        {
            var allCategories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<List<CategoryDto>>(allCategories);
        }

        public async Task UpdateCategory(CategoryDto categoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryDto.Id); // Id'yi bulma komutu. GetById
            category.CategoryName = categoryDto.CategoryName;


            await _categoryRepository.UpdateAsync(category);
        }
    }
}
