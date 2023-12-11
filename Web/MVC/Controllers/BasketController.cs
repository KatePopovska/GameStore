using Microsoft.AspNetCore.Mvc;
using MVC.Services.Interfaces;
using MVC.ViewModels.BasketViewModel;
using System.Security.Cryptography.Xml;

namespace MVC.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IBasketService basketService, ILogger<BasketController> logger)
        {
            _basketService = basketService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _basketService.GetBasket();
            return View(result);
        }
        public async Task<IActionResult> Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToBasket(int productId, string productName, int quantity, int price, string pictureUrl)
        {
            var basketItem = new BasketItemModel()
            {
                ProductId = productId,
                ProductName = productName,
                Quantity = quantity,
                Price = price
            };

            await _basketService.AddToBasket(basketItem);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmationCheckout(string userName, string givenName, string familyName, string email, string country, string state, string zipCode)
        {
            var checkoutModel = new BasketCheckoutModel()
            {
                UserName = userName,
                GivenName = givenName,
                FamilyName = familyName,
                EmailAddress = email,
                Country = country,
                State = state,
                ZipCode = zipCode,
                TotalPrice = 30
            };
            _logger.LogInformation($"Log from ConfirmationCheckout BasketController");
            await _basketService.CheckoutBasket(checkoutModel);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteItem(int productId)
        {
            await _basketService.DeleteItem(productId);
            return RedirectToAction("Index");
        }
    }
}
