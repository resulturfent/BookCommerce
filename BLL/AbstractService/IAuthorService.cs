using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AbstractService
{
    public interface IAuthorService
    {
        Task CreateAuthor(AuthorDto authorDto);
        Task<List<AuthorDto>> GetAllAuthors();
        Task DeleteAuthor(int authorId);
        Task UpdateAuthor(AuthorDto authorDto);
    }
}
