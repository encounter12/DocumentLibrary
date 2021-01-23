using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentLibrary.DTO.DTOs;

namespace DocumentLibrary.Services.Contracts
{
    public interface IGenreService
    {
        Task<List<GenreDto>> GetGenresAsync();
    }
}