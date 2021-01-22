﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentLibrary.Data.Entities;
using DocumentLibrary.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Book = DocumentLibrary.Data.Entities.Book;

namespace DocumentLibrary.API.Admin.Controllers
{
    [ApiController]
    [Route("Admin/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("/Books")]
        public async Task<ActionResult<Book>> GetBooksAsync()
        {
            var books = await _bookService.GetBooksAsync();

            return Ok(books);
        }
        
        [HttpGet("/Pencils")]
        public IAsyncEnumerable<Pencil> GetPencilsAsync()
        {
            var books = _bookService.GetPencilsAsAsyncEnumerable();

            return books;
        }
    }
}