using AutoMapper;
using DocumentLibrary.API.Admin.ViewModels;
using DocumentLibrary.DTO.DTOs;

namespace DocumentLibrary.API.Admin.AutoMapper
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<BookPostModel, BookPostDto>();
            CreateMap<BooksGridDto, BooksGridViewModel>();
        }
    }
}