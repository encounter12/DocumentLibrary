using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DocumentLibrary.API.Admin.ViewModels;
using DocumentLibrary.DTO.DTOs;
using DocumentLibrary.Infrastructure.AspNetHelpers.Contracts;
using DocumentLibrary.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DocumentLibrary.API.Admin.Controllers
{
    [ApiController]
    [Route("Admin/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        
        private readonly IModelStateErrorHandler _modelStateErrorHandler;
        
        private readonly IMapper _mapper;

        public BooksController(
            IBookService bookService,
            IModelStateErrorHandler modelStateErrorHandler,
            IMapper mapper)
        {
            _bookService = bookService;
            _modelStateErrorHandler = modelStateErrorHandler;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetBooks()
        {
            try
            {
                List<BookListDto> books = await _bookService.GetBooksAsync();
                var booksListViewModel = _mapper.Map<List<BookListViewModel>>(books);
                
                return Ok(booksListViewModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> PostBook(BookPostModel bookPostModel)
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = _modelStateErrorHandler.GetErrors(ModelState);
                return BadRequest(errors);
            }

            long bookId;
            
            try
            {
                var bookPostDto = _mapper.Map<BookPostDto>(bookPostModel);
                bookId = await _bookService.AddBookAsync(bookPostDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok(bookId);
        }
    }
}