using System.Collections.Generic;
using Shared.DTOS;

namespace Services
{
    public interface IBooksServices
    {
        void CreateBook(BookDto book);
        void DeleteBook(int id);
        BookDto GetBook(int id);
        void UpdateBook(int id, BookDto book);
        List<BookDto> GetAllBooks();
    }
}