using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentLibrary.Data.Context;
using DocumentLibrary.Data.Entities;
using DocumentLibrary.Data.Repositories.Contracts;
using DocumentLibrary.DTO.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DocumentLibrary.Data.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        private readonly DocumentLibraryContext _context;
        
        private readonly IMapper _mapper;

        public GenreRepository(DocumentLibraryContext context, IMapper mapper): base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<List<GenreDto>> GetGenresAsync()
        {
            var genres = await this
                .All()
                .ProjectTo<GenreDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return genres;
        }
    }
}