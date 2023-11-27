using Microsoft.AspNetCore.Mvc;
using Order.API.DTOs;
using Order.API.Services;
using Order.Data.Interfaces;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly OrderService _orderService;

        public OrderController(ILogger<OrderController> logger, OrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(CreateOrderDTO dto)
        {
            if(await _orderService.AddOrder(dto))
                return Ok(dto);
            return BadRequest();
        }


        [HttpGet]
        public ActionResult GetAllOrders()
        {
            return Ok(_orderService.GetAllOrders());
        }
    }
}
