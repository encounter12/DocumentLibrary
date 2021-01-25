﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using DocumentLibrary.API.Public.ViewModels;
using DocumentLibrary.DTO.DTOs;
using DocumentLibrary.Infrastructure.AspNetHelpers.Contracts;
using DocumentLibrary.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DocumentLibrary.API.Public.Controllers
{
    [ApiController]
    [Route("Public/[controller]")]
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
        public async Task<ActionResult> GetBooks(int pageNumber, int itemsPerPage)
        {
            try
            {
                BooksGridDto books = await _bookService.GetBooksAsync(pageNumber, itemsPerPage);
                var booksListViewModel = _mapper.Map<BooksGridViewModel>(books);
                
                return Ok(booksListViewModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}