using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Entities.Entities;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Services;
using Shared;
using Shared.DTOS;


namespace UnitTests
{
    [TestClass]
    public class BooksTest : PrepairTests
    {

        [TestInitialize]
        public void Init()
        {
            //Custom init
        }

        [TestMethod]
        public void GetBook()
        {
            var bookDto = _booksServices.GetBook(2);
            Assert.IsNotNull(bookDto);
        }

        [TestMethod]
        public void GetAllBook()
        {
            Assert.IsTrue(_booksServices.GetAllBooks().Any());
        }

        [TestMethod]
        public void CreateBook()
        {
            _booksServices.CreateBook(new BookDto(){ChategoryId = 1, Author = "TestAut", Name = "TestName", Price = 40});
            
            Assert.IsTrue(_books.Queryable().Any(x => x.Author == "TestAut" && x.Name == "TestName"));
        }

        [TestMethod]
        public void UpdateBook()
        {
            _booksServices.UpdateBook(1, new BookDto(){Name = "NewName"});

            Assert.IsTrue(_booksServices.GetBook(1).Name == "NewName");
        }

        [TestMethod]
        public void DeleteBook()
        {
            _booksServices.DeleteBook(2);

            Assert.IsNull(_booksServices.GetBook(2)); 
        }
    }
}
