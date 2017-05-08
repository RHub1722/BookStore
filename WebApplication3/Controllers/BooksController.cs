using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Services;
using Shared.DTOS;

namespace BookStore.Controllers
{
    [Route("api/Books")]
    public class BooksController : Controller
    {
        private readonly IBooksServices _booksServices;

        public BooksController(IBooksServices booksServices)
        {
            _booksServices = booksServices;
        }

        /// <summary>
        /// GetBook
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetByID")]
        [HttpGet]
        public IActionResult GetByID(int id)
        {
            BookDto bookDto = _booksServices.GetBook(id);
            if (bookDto == null)
                return NotFound();

            return new ObjectResult(bookDto);
        }

        /// <summary>
        /// GetAllBooks
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetAllBooks")]
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            List<BookDto> bookDto = _booksServices.GetAllBooks();
            if (bookDto == null)
                return NotFound();

            return new ObjectResult(bookDto);
        }


        /// <summary>
        /// Create
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [CustomAuthorize("Admin")]
        [Route("Create")]
        [HttpPost]
        public IActionResult Create([FromBody] BookDto item)
        {
            if (item == null)
                return BadRequest();

            _booksServices.CreateBook(item);
            return Ok();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [CustomAuthorize("Admin")]
        [Route("Update")]
        [HttpPost]
        public IActionResult Update(int id, [FromBody] BookDto item)
        {
            var bookDto = _booksServices.GetBook(id);
            if (bookDto == null)
                return NotFound();

            _booksServices.UpdateBook(id, item);
            return Ok();
        }


        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [CustomAuthorize("Admin")]
        [Route("Delete")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _booksServices.DeleteBook(id);
            return Ok();
        }

    }
}
