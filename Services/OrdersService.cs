using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Entities;
using Entities.Entities;
using Repository.Interfaces;
using Shared.DTOS;
using System.Linq;

namespace Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Order> _order;
        private readonly IRepository<Book> _books;

        public OrdersService(IMapper mapper, IRepository<Order> order, IRepository<Book> books)
        {
            _mapper = mapper;
            _order = order;
            _books = books;
        }

        public void CreateOrder(int userId, int bookId, int qty)
        {
            var order = new Order();
            order.UserId = userId;
            order.BookId = userId;
            order.Qty = qty;
            order.TotalPrice = qty * _books.Queryable().Where(x => x.Id == bookId).Select(x => x.Price).First();//Do not use First(x =>x.Id == userId).Price

            _order.Insert(order);
           _order.Save();
        }

        public List<OrderDto> GetOrders(int userId)
        {
            var orderDtos = new List<OrderDto>();
            _order.Queryable().Where(x => x.UserId == userId).ToList().ForEach(x => orderDtos.Add(_mapper.Map<OrderDto>(x)));
          
            if (!orderDtos.Any())
                return null;

            return orderDtos;
        }

        public List<OrderDto> GetOrders()
        {
            var orderDtos = new List<OrderDto>();
            _order.Queryable().ToList().ForEach(x => orderDtos.Add(_mapper.Map<OrderDto>(x)));

            if (!orderDtos.Any())
                return null;

            return orderDtos;
        }

        public void UpdateOrder(int orderId, OrderDto item)
        {
            var resp = _order.Queryable().FirstOrDefault(x => x.Id == orderId);
            if (resp == null)
                return;
            resp.BookId = item.BookId;
            resp.Qty = item.Qty;
            resp.TotalPrice = item.TotalPrice;
            resp.UserId = item.UserId;
            resp.ObjectState = ObjectState.Modified;
            _order.Save();
        }

        public void DeleteOrder(int orderId)
        {
            var resp = _order.Queryable().FirstOrDefault(x => x.Id == orderId);
            if (resp == null)
                return;
            _order.Delete(resp);
            _order.Save();
        }
    }
}
