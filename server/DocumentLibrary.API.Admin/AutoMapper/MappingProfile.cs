using AutoMapper;
using DocumentLibrary.API.Admin.ViewModels;
using DocumentLibrary.DTO.DTOs;

namespace DocumentLibrary.API.Admin.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookPostModel, BookPostDto>();
            CreateMap<BookListDto, BookListViewModel>();
        }
    }
}