using BLL.Dtos;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AbstractService
{
    public interface IBookService
    {
        Task CreateBook(BookDto bookDto);
        Task<List<BookDto>> GetAllBooks();
        Task DeleteBook(int bookId);
        Task UpdateBook(BookDto bookDto);
        Task AddBookAuthor(BookAuthorDto bookAuthorDto);
        Task AddBookCategory(BookCategoryDto bookCategoryDto);
        Task<Book> GetBookById(int bookId); 
       //decimal GetBookPriceById(int bookId);



    }
}
