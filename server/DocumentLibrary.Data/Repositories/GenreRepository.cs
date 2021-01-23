using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public GenreRepository(DocumentLibraryContext context) : base(context)
        {
            _context = context;
        }
        
        public async Task<List<GenreDto>> GetGenresAsync()
        {
            var genres = await this
                .All()
                .Select(b => new GenreDto
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToListAsync();

            return genres;
        }
    }
}