using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentLibrary.DTO.DTOs;
using DocumentLibrary.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DocumentLibrary.API.Admin.Controllers
{
    [ApiController]
    [Route("Admin/[controller]")]
    public class GenresController: ControllerBase
    {
        private readonly IGenreService _genreService;
        
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        
        [HttpGet]
        public async Task<ActionResult> GetGenres()
        {
            List<GenreDto> genres = await _genreService.GetGenresAsync();
            return Ok(genres);
        }
    }
}