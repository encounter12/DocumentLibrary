using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentLibrary.Data.Repositories.Contracts;
using DocumentLibrary.DTO.DTOs;
using DocumentLibrary.Services.Contracts;

namespace DocumentLibrary.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        
        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        
        public async Task<List<GenreDto>> GetGenresAsync() 
            => await _genreRepository.GetGenresAsync();
    }
}