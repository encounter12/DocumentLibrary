using System.Linq;
using AutoMapper;
using DocumentLibrary.Data.Entities;
using DocumentLibrary.DTO.DTOs;

namespace DocumentLibrary.Data.AutoMapper
{
    public class DataMappingProfile : Profile
    {
        public DataMappingProfile()
        {
            CreateMap<BookPostDto, Book>().ForMember(dest =>
                    dest.Keywords,
                opt=> opt.MapFrom(src => src.Keywords.Select(x => new Keyword
                {
                    Name = x
                })));

            CreateMap<Genre, GenreDto>();
        }
    }
}