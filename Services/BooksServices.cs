using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Entities;
using Entities.Entities;
using Repository.Interfaces;
using Shared.DTOS;

namespace Services
{
    public class BooksServices : IBooksServices
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Book> _books;
        private IBooksServices _booksServicesImplementation;

        public BooksServices(IMapper mapper, IRepository<Book> books)
        {
            _mapper = mapper;
            _books = books;
        }

        public void CreateBook(BookDto book)
        {
            var map = _mapper.Map<Book>(book);
            _books.Insert(map);
            _books.Save();
        }

        public BookDto GetBook(int id)
        {
            var resp = _books.Queryable().FirstOrDefault(x =>x.Id == id);
            if (resp == null)
                return null;

            return _mapper.Map<BookDto>(resp);
        }

        public void UpdateBook(int id, BookDto book)
        {
            var resp = _books.Queryable().FirstOrDefault(x => x.Id == id);
            if (resp == null)
                return;
            resp.ChategoryId = book.ChategoryId;
            resp.Author = book.Author;
            resp.Name = book.Name;
            resp.Price = book.Price;
            resp.ObjectState = ObjectState.Modified;
            _books.Save();
        }

        public List<BookDto> GetAllBooks()
        {
            var bookDtos = new List<BookDto>();
            _books.Queryable().ToList().ForEach(x => bookDtos.Add(_mapper.Map<BookDto>(x)));
            if (!bookDtos.Any())
                return null;
            return bookDtos;
        }

        public void DeleteBook(int id)
        {
            var resp = _books.Queryable().FirstOrDefault(x => x.Id == id);
            if (resp == null)
                return;
            _books.Delete(resp);
            _books.Save();
        }

    }
}
