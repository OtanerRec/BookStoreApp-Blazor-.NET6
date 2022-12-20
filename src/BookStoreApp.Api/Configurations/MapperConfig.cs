using AutoMapper;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.DTO.Author;
using BookStoreApp.Api.DTO.Book;
using BookStoreApp.Api.DTO.User;

namespace BookStoreApp.Api.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorReadOnlyDTO, Author>().ReverseMap();
            CreateMap<AuthorCreateDTO, Author>().ReverseMap();
            CreateMap<AuthorUpdateDTO, Author>().ReverseMap();

            CreateMap<BookCreateDTO, Book>().ReverseMap();
            CreateMap<BookUpdateDTO, Book>().ReverseMap();

            CreateMap<Book, BookReadOnlyDTO>()
                .ForMember(o => o.AuthorName, d => d.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
                .ReverseMap();

            CreateMap<Book, BookDetailsDTO>()
                .ForMember(o => o.AuthorName, d => d.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
                .ReverseMap();

            CreateMap<UserDto, ApiUser>().ReverseMap();

        }
    }
}
