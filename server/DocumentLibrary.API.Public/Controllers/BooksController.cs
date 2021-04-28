using System;
using System.Threading.Tasks;
using AutoMapper;
using DocumentLibrary.API.Public.ViewModels;
using DocumentLibrary.DTO.DTOs;
using DocumentLibrary.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DocumentLibrary.API.Public.Controllers
{
    [ApiController]
    [Route("Public/[controller]")]
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
        
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetBookDetails(long id)
        {
            try
            {
                BookDetailsDto bookDetailsDto = await _bookService.GetBookDetailsAsync(id);
                BookDetailsViewModel bookDetailsViewModel = _mapper.Map<BookDetailsViewModel>(bookDetailsDto);
                
                return Ok(bookDetailsViewModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        //TODO: Implement action methods: CheckoutBook (PUT), DownloadBook (GET),
        //SearchForBooks (GET) - search using Azure Cognitive Search 
        
        [HttpPut]
        [Route("Checkout/{id}")]
        public async Task<ActionResult> Checkout(long id)
        {
            try
            {
                await Task.Delay(100);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}