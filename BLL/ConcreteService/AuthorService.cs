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
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorRepsitory;
        private readonly IMapper _mapper;

        public AuthorService(IRepository<Author> authorRepsitory, IMapper mapper)
        {
            _authorRepsitory = authorRepsitory;
            _mapper = mapper;
        }
        public async Task CreateAuthor(AuthorDto authorDto)
        {
            var author = _mapper.Map<Author>(authorDto);
            await _authorRepsitory.AddAsync(author);
        }

        public async Task DeleteAuthor(int authorId)
        {
            //var author = await _authorRepsitory.GetAllAsyns();
            //foreach (var item in author)
            //{
            //    await _authorRepsitory.DeleteAsync(item.Id);
            //}
            var authors = await _authorRepsitory.GetByIdAsync(authorId);
            if (authors != null)
            {
                await _authorRepsitory.DeleteAsync(authorId);
            }
        }

        public async Task<List<AuthorDto>> GetAllAuthors()
        {
            var allAuthors = await _authorRepsitory.GetAllAsync();
            return _mapper.Map<List<AuthorDto>>(allAuthors);

        }

        public async Task UpdateAuthor(AuthorDto authorDto)
        {
            var author = await _authorRepsitory.GetByIdAsync(authorDto.Id); // Id'yi bulma komutu. GetById
            author.AuthorName = authorDto.AuthorName;
            author.AuthorSurname = authorDto.AuthorSurname;

            await _authorRepsitory.UpdateAsync(author);
        }
    }
}
