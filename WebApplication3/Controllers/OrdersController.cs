using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Services;
using Shared.DTOS;

namespace BookStore.Controllers
{
    [Route("api/Orders")]
    public class OrdersController : Controller 
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        /// <summary>
        /// CreateOrder
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>
        /// <param name="qty"></param>
        [CustomAuthorize()]
        [Route("CreateOrder")]
        [HttpPost]
        public IActionResult CreateOrder(int bookId, int qty)
        {
            var id = int.Parse(HttpContext.Request.Headers.First(x => x.Key == "userid").Value.ToString());

            _ordersService.CreateOrder(id, bookId, qty);

            return Ok();
        }
        /// <summary>
        /// DeleteOrder
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [CustomAuthorize("Admin")]
        [Route("DeleteOrder")]
        [HttpDelete]
        public IActionResult DeleteOrder(int orderId)
        {
            _ordersService.DeleteOrder(orderId);

            return Ok();
        }
        /// <summary>
        /// GetAllOrders
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize("Admin")]
        [Route("GetAllOrders")]
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orderDtos = _ordersService.GetOrders();
            if (orderDtos == null)
                return NotFound();

            return new ObjectResult(orderDtos);
        }

        /// <summary>
        /// GetOrders
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize()]
        [Route("GetOrdersByCurrentUser")]
        [HttpGet]
        public IActionResult GetOrders()
        {
            var id = int.Parse(HttpContext.Request.Headers.First(x => x.Key == "userid").Value.ToString());

            var orderDtos = _ordersService.GetOrders(id);
            if (orderDtos == null)
                return NotFound();

            return new ObjectResult(orderDtos);
        }

        /// <summary>
        /// UpdateOrder
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [CustomAuthorize("Admin")]
        [Route("UpdateOrder")]
        [HttpPost]
        public IActionResult UpdateOrder(int orderId, [FromBody]OrderDto item)
        {
            _ordersService.UpdateOrder(orderId, item);
            return Ok();
        }
    }
}