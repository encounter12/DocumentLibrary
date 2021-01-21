using System.Collections.Generic;
using DocumentLibrary.API.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentLibrary.API.Admin.Controllers
{
    [ApiController]
    [Route("Admin/[controller]")]
    public class BooksController : ControllerBase
    {
        public BooksController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            var books = new List<Book>
            {
                new Book
                {
                    Name = "Foundation"
                },
                new Book
                {
                    Name = "The Call of Ktulu"
                }
            };
            
            return Ok(books);
        }
    }
}