using AutoMapper;
using DocumentLibrary.API.Public.ViewModels;
using DocumentLibrary.DTO.DTOs;

namespace DocumentLibrary.API.Public.AutoMapper
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<BookListDto, BookListViewModel>();
        }
    }
}