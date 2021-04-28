using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DocumentLibrary.API.Admin.ViewModels;
using DocumentLibrary.DTO.DTOs;
using DocumentLibrary.Infrastructure.AspNetHelpers.Contracts;
using DocumentLibrary.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentLibrary.API.Admin.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Admin/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        private readonly IMapper _mapper;

        private readonly IModelStateErrorHandler _modelStateErrorHandler;

        public BooksController(IBookService bookService, IMapper mapper, IModelStateErrorHandler modelStateErrorHandler)
        {
            _bookService = bookService;
            _mapper = mapper;
            _modelStateErrorHandler = modelStateErrorHandler;
        }

        [HttpGet]
        public async Task<ActionResult> GetBooks(
            int? pageNumber,
            int? itemsPerPage,
            string search,
            DateTime? fromDate,
            DateTime? toDate,
            string sortOrder,
            string sortBy
            )
        {
            try
            {
                var queryFilterDto = new QueryFilterDto
                {
                    PageNumber = pageNumber,
                    ItemsPerPage = itemsPerPage,
                    Search = search,
                    FromDate = fromDate,
                    ToDate = toDate,
                    SortOrder = sortOrder,
                    SortBy = sortBy
                };

                BooksGridDto books = await _bookService.GetBooksAsync(queryFilterDto);
                
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
        
        [HttpPut]
        public async Task<ActionResult> PutBook(BookEditViewModel bookEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = _modelStateErrorHandler.GetErrors(ModelState);
                return BadRequest(errors);
            }
            
            try
            {
                var bookEditDto = _mapper.Map<BookEditDto>(bookEditViewModel);
                await _bookService.UpdateBookAsync(bookEditDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
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