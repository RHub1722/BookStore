using System;
using System.Collections.Generic;
using System.Text;
using Entities.Entities;
using Shared.DTOS;
using AutoMapper;

namespace Shared
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
        }

    }
}
