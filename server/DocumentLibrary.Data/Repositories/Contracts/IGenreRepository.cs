using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentLibrary.Data.Entities;
using DocumentLibrary.DTO.DTOs;

namespace DocumentLibrary.Data.Repositories.Contracts
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<List<GenreDto>> GetGenresAsync();
    }
}