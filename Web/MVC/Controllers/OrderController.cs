using Microsoft.AspNetCore.Mvc;
using MVC.Services.Interfaces;
using MVC.ViewModels.OrderViewModels;

namespace MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetOrdersByUserName();
            var vm = new OrderIndexViewModel()
            {
                Orders = orders.ToList(),
                Count = orders.Count()
            };
            return View(vm);
        }
    }
}
