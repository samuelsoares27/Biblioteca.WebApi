using AutoMapper;
using Biblioteca.Models.Models;
using Biblioteca.WebApi.Dto;

namespace Biblioteca.WebApi.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Livros, LivroDto>().ReverseMap();
        }
    }
}
