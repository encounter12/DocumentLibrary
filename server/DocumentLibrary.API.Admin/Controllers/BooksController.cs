using System;
using System.Collections.Generic;
using System.Linq;
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

        private readonly IPageFilterValidator _pageFilterValidator;
        
        private readonly IMapper _mapper;

        public BooksController(
            IBookService bookService,
            IModelStateErrorHandler modelStateErrorHandler,
            IPageFilterValidator pageFilterValidator,
            IMapper mapper)
        {
            _bookService = bookService;
            _modelStateErrorHandler = modelStateErrorHandler;
            _pageFilterValidator = pageFilterValidator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetBooks(int pageNumber, int itemsPerPage)
        {
            try
            {
                int allRecordsCount = await _bookService.GetAllRecordsCountAsync();
                
                IEnumerable<string> validationErrors =
                    _pageFilterValidator.Validate(pageNumber, itemsPerPage, allRecordsCount);

                if (validationErrors.Any())
                {
                    return BadRequest(validationErrors);
                }

                BooksGridDto books = await _bookService.GetBooksAsync(pageNumber, itemsPerPage);
                var booksGridViewModel = _mapper.Map<BooksGridViewModel>(books);
                
                return Ok(booksGridViewModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        //TODO: Implement file upload in action method: PostBook(), implement upload to Azure Blob Storage and
        //automatic blob indexing using Azure Cognitive Search 
        
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

        [HttpDelete]
        public async Task<ActionResult> DeleteBook(long bookId)
        {
            if (bookId <= 0)
            {
                return BadRequest("The operation cannot be completed. Non-existing BookId");
            }
            
            try
            {
                await _bookService.DeleteBook(bookId);
                return Ok(bookId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}