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
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<BookAuthor> _bookAuthorRepository;
        private readonly IRepository<BookCategory> _bookCategoryRepository;

        public BookService(IRepository<Book> bookRepository, IMapper mapper, IRepository<BookAuthor> bookAuthorRepository, IRepository<BookCategory> bookCategoryRepository)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _bookAuthorRepository = bookAuthorRepository;
            _bookCategoryRepository = bookCategoryRepository;
        }

        public async Task AddBookAuthor(BookAuthorDto bookAuthorDto)
        {

            await _bookAuthorRepository.AddAsync(_mapper.Map<BookAuthor>(bookAuthorDto));
        }

        public async Task AddBookCategory(BookCategoryDto bookCategoryDto)
        {
            await _bookCategoryRepository.AddAsync(_mapper.Map<BookCategory>(bookCategoryDto));
        }

        public async Task CreateBook(BookDto bookDto)
        {

            if (bookDto.UserId == 0)
            {
                throw new Exception("UserId is required to create a book.");
            }
            var book = _mapper.Map<Book>(bookDto);
            await _bookRepository.AddAsync(book);
        }

        public async Task DeleteBook(int bookId)
        {
            //var book = await _bookRepository.GetByIdAsync(bookId);
            //foreach (var item in book)
            //{
            //    await _bookRepository.DeleteAsync(item.Id);
            //}
            await _bookRepository.DeleteAsync(bookId);
        }

        public async Task<List<BookDto>> GetAllBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            //  books = books.Where(x => x.IsApproved); Onaylı olanları göstermek için bunu kullancaz.
            return _mapper.Map<List<BookDto>>(books);
        }

        public async Task<Book> GetBookById(int bookId)
        {
            var getBook=await _bookRepository.GetByIdAsync(bookId);

            return _mapper.Map<Book>(getBook);
        }

        public  async Task<decimal> GetBookPriceById(int bookId)
        {
            var result =await  _bookRepository.GetByIdAsync(bookId);

            return  result.UnitPrice;
        }

        public async Task UpdateBook(BookDto bookDto)
        {
            var book = await _bookRepository.GetByIdAsync(bookDto.Id); // Id'yi bulma komutu. GetById
            book.Title = bookDto.Title;
            book.UnitPrice = bookDto.UnitPrice;
            book.UnitInStocks = bookDto.UnitInStocks;
            //   book.Author.AuthorName = bookDto.AuthorDto.AuthorName;
            book.ISBN = bookDto.ISBN;
            book.Photo = bookDto.Photo;
            book.Description = bookDto.Description;
            book.PublishingHouse = bookDto.PublishingHouse;
            book.PublishedDate = bookDto.PublishedDate;
            //  book.Category.CategoryName = bookDto.CategoryDto.CategoryName;

        }

    }
}
