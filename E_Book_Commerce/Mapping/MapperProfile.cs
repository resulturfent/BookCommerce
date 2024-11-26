using AutoMapper;
using BLL.Dtos;
using Book_Commerce.Models;
using DAL.Entities;

namespace Book_Commerce.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserDto, UserViewModel>().ReverseMap();
            CreateMap<AuthorDto, AuthorViewModel>().ReverseMap();
            CreateMap<BookDto, BookViewModel>().ReverseMap();
            CreateMap<BookAuthorDto, BookAuthorViewModel>().ReverseMap();
            CreateMap<BookCategoryDto, BookCategoryViewModel>().ReverseMap();
            CreateMap<CategoryDto, CategoryViewModel>().ReverseMap();
            CreateMap<CartDto,CartItemViewModel>().ReverseMap();
            CreateMap<CartItemDto, CartItemViewModel>().ReverseMap();
            CreateMap<CartDto, CartViewModel>().ReverseMap(); 
            //CreateMap<Book, BookViewModel>().ReverseMap();


        }
    }
}
