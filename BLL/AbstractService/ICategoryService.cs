using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AbstractService
{
    public interface ICategoryService
    {
        Task CreateCategory(CategoryDto categoryDto);
        Task<List<CategoryDto>> GetAllCategories();
        Task DeleteCategory(int categoryId);
        Task UpdateCategory(CategoryDto categoryDto);
    }
}
