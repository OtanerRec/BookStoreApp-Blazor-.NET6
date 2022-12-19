using AutoMapper;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.DTO.Author;

namespace BookStoreApp.Api.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorReadOnlyDTO, Author>().ReverseMap();
            CreateMap<AuthorCreateDTO, Author>().ReverseMap();
            CreateMap<AuthorUpdateDTO, Author>().ReverseMap();
        }
    }
}
