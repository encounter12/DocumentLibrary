using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DocumentLibrary.API.Admin.ViewModels;
using DocumentLibrary.DTO.DTOs;
using DocumentLibrary.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DocumentLibrary.API.Admin.Controllers
{
    [ApiController]
    [Route("Admin/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetBooks()
        {
            List<BookListDto> books = await _bookService.GetBooksAsync();
            var booksListViewModel = _mapper.Map<List<BookListViewModel>>(books);
            return Ok(booksListViewModel);
        }
        
        [HttpPost]
        public async Task<ActionResult> PostBook(BookPostModel bookPostModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = GetErrors(ModelState);
                return BadRequest(errors);
            }
            
            var bookPostDto = _mapper.Map<BookPostDto>(bookPostModel);
            await _bookService.AddBookAsync(bookPostDto);
            return Ok();
        }

        private List<string> GetErrors(ModelStateDictionary modelState)
            => modelState.Values
                .SelectMany(msv => msv.Errors)
                .Select(x => x.ErrorMessage)
                .ToList();
        
    }
}