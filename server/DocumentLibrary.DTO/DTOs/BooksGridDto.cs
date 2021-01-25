using System.Collections.Generic;

namespace DocumentLibrary.DTO.DTOs
{
    public class BooksGridDto
    {
        public List<BookListDto> BooksList { get; set; }

        public int PagesCount { get; set; }
    }
}