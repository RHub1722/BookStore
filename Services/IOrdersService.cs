using System.Collections.Generic;
using Shared.DTOS;

namespace Services
{
    public interface IOrdersService
    {
        void CreateOrder(int userId, int bookId, int qty);
        void DeleteOrder(int orderId);
        List<OrderDto> GetOrders();
        List<OrderDto> GetOrders(int userId);
        void UpdateOrder(int orderId, OrderDto item);
    }
}