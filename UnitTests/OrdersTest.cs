using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using Shared.DTOS;

namespace UnitTests
{
    [TestClass]
    public class OrdersTest : PrepairTests
    {
        [TestMethod]
        public void CreateOrder()
        {
            _ordersService.CreateOrder(1, 1, 10);
            var orderDtos = _ordersService.GetOrders(1).FirstOrDefault(x =>x.Qty == 10 && x.BookId == 1);
           Assert.IsNotNull(orderDtos);
        }

        [TestMethod]
        public void DeleteOrder()
        {
            _ordersService.DeleteOrder(1);
            Assert.IsFalse(_ordersService.GetOrders().Any(x => x.Id == 1));
        }

        [TestMethod]
        public void GetOrders()
        {
            Assert.IsTrue(_ordersService.GetOrders().Any());
        }

        [TestMethod]
        public void GetOrdersByUserId()
        {
            Assert.IsTrue(_ordersService.GetOrders(2).Count == 1);
        }

        [TestMethod]
        public void UpdateOrder()
        {
            _ordersService.UpdateOrder(1, new OrderDto(){Qty = 123, BookId = 5});
            Assert.IsNotNull(_ordersService.GetOrders().FirstOrDefault(x =>x.BookId == 5 && x.Qty == 123));
        }
    }
}
