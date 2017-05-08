using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Entities.Entities;
using Repository;
using Services;
using Shared;

namespace UnitTests
{
    public class PrepairTests
    {
        protected FakeRepository<Book> _books;
        protected BooksServices _booksServices;
        protected FakeRepository<User> _users;
        protected FakeRepository<Category> _categories;
        protected FakeRepository<Order> _orders;
        protected CategoriesService _categoriesService;
        protected OrdersService _ordersService;

        public PrepairTests()
        {
            int counter = 1;
            var mapper = new Mapper(new MapperConfiguration(x => x.AddProfile(new MappingProfile())));
            var categories = new List<Category>()
            {
                new Category(){Name = "Category 1", Id = counter++},
                new Category(){Name = "Category 2", Id = counter++},
            };
            _categories = new FakeRepository<Category>(categories);

            counter = 1;

            var books = new List<Book>();

           
            for (int i = 0; i < 10; i++)
                books.Add(new Book() { ChategoryId = 1, Author = "Author", Name = "Name", Id = counter++, Price = i + 10 });

            for (int i = 0; i < 10; i++)
                books.Add(new Book() { ChategoryId = 2, Author = "Author2", Name = "Name2", Id = counter++, Price = i + 10 });
            _books = new FakeRepository<Book>(books);

            counter = 1;

            var users = new List<User>()
            {
                new User(){Role = "Admin", Id = counter++},
                new User(){Role = "User", Id = counter++},
                new User(){Role = "User", Id = counter++},
            };
            _users = new FakeRepository<User>(users);

            counter = 1;

            var orders = new List<Order>()
             {
                 new Order(){BookId = 1, Qty = 5,Id = counter++ ,TotalPrice = 500, UserId = 1},
                 new Order(){BookId = 2, Qty = 1,Id = counter++ ,TotalPrice = 500,  UserId = 1},
                 new Order(){BookId = 3, Qty = 3,Id = counter++ ,TotalPrice = 200,  UserId = 2},
             };
            _orders = new FakeRepository<Order>(orders);

            _booksServices = new BooksServices(mapper, _books);
            _categoriesService = new CategoriesService(mapper, _categories);
            _ordersService = new OrdersService(mapper, _orders, _books);
        }
    }
}
