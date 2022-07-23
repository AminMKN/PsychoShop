using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using PsychoShop.Application.Contracts.Order;
using PsychoShop.Application.Contracts.ShopCart;
using PsychoShop.Framework.Application;
using PsychoShop.Framework.Application.ZarinPal;
using PsychoShop.Query.Contract;
using PsychoShop.Query.Contract.Product;
using ServiceHost.Models;
using System.Globalization;

namespace ServiceHost.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly IOrderApplication _orderApplication;
        private readonly ICartCalculatorService _cartCalculatorService;

        public PaymentController(IProductQuery productQuery, IOrderApplication orderApplication, ICartCalculatorService cartCalculatorService, ICartService cartService, IZarinPalFactory zarinPalFactory)
        {
            _productQuery = productQuery;
            _orderApplication = orderApplication;
            _cartCalculatorService = cartCalculatorService;
            _cartService = cartService;
            _zarinPalFactory = zarinPalFactory;
        }

        public async Task<IActionResult> Cart()
        {
            var command = new PaymentCommand();
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[command.CookieName];
            if (value == "[]")
            {
                command.EmptyCart = ApplicationMessages.CartIsEmpty;
                return View(command);
            }

            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            if (cartItems == null)
            {
                command.EmptyCart = ApplicationMessages.CartIsEmpty;
                return View(command);
            }

            foreach (var item in cartItems)
                item.CalculateTotalItemPrice();

            command.CartItems = await _productQuery.CheckInventoryStatus(cartItems);
            return View(command);
        }

        public IActionResult RemoveFromCart(int id)
        {
            var command = new PaymentCommand();
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[command.CookieName];
            Response.Cookies.Delete(command.CookieName);
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            var itemToRemove = cartItems.FirstOrDefault(x => x.Id == id);
            cartItems.Remove(itemToRemove);
            var options = new CookieOptions { Expires = DateTime.Now.AddDays(5) };
            Response.Cookies.Append(command.CookieName, serializer.Serialize(cartItems), options);
            return RedirectToAction("Cart", "Payment");
        }

        public async Task<IActionResult> GoToCheckout()
        {
            var command = new PaymentCommand();
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[command.CookieName];
            if (value == "[]")
            {
                command.EmptyCart = ApplicationMessages.CartIsEmpty;
                return RedirectToAction("Cart", "Payment");
            }

            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            if (cartItems == null)
            {
                command.EmptyCart = ApplicationMessages.CartIsEmpty;
                return RedirectToAction("Cart", "Payment");
            }

            foreach (var item in cartItems)
                item.CalculateTotalItemPrice();

            command.CartItems = await _productQuery.CheckInventoryStatus(cartItems);
            if (command.CartItems.Any(x => !x.InStock))
                return RedirectToAction("Cart", "Payment");

            return RedirectToAction("Checkout", "Payment");
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var command = new PaymentCommand();
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[command.CookieName];
            if (value == "[]")
                return RedirectToAction("Cart", "Payment");

            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            if (cartItems == null)
                return RedirectToAction("Cart", "Payment");

            foreach (var item in cartItems)
                item.CalculateTotalItemPrice();

            command.Cart = _cartCalculatorService.ComputeCart(cartItems);
            _cartService.Set(command.Cart);
            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(string postalCode, string address,string phoneNumber)
        {
            var cart = _cartService.Get();
            var result = await _productQuery.CheckInventoryStatus(cart.CartItems);
            if (result.Count == 0)
                return RedirectToPage("/Cart");

            if (result.Any(x => !x.InStock))
                return RedirectToPage("/Cart");

            cart.PhoneNumber = phoneNumber;
            cart.PostalCode = postalCode;
            cart.Address = address;
            var orderId = _orderApplication.PlaceOrder(cart);
            var paymentResponse = _zarinPalFactory.CreatePaymentRequest(cart.PayAmount.ToString(CultureInfo.InvariantCulture),
                 cart.PhoneNumber, "", "خرید از سایت سایکو شاپ", orderId);

            return Redirect($"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
        }

        public IActionResult CallBack([FromQuery] string authority, [FromQuery] string status, [FromQuery] int oId)
        {
            var command = new PaymentCommand();
            var result = new PaymentResult();
            var orderAmount = _orderApplication.GetAmount(oId);
            var verificationResponse = _zarinPalFactory.CreateVerificationRequest(authority, orderAmount.ToString(CultureInfo.InvariantCulture));
            if (status == "OK" && verificationResponse.Status >= 100)
            {
                var issueTrackingNo = _orderApplication.PaymentSuccess(oId, verificationResponse.RefId);
                Response.Cookies.Delete(command.CookieName);
                result = result.Success(ApplicationMessages.PaymentSuccess, issueTrackingNo);
                return RedirectToAction("PaymentResult","Payment", result);
            }

            result = result.Failed(ApplicationMessages.PaymentFailed);
            return RedirectToAction("PaymentResult", "Payment", result);
        }

        public IActionResult PaymentResult(PaymentResult paymentResult)
        {
            var command = new PaymentCommand()
            {
                PaymentResult = paymentResult
            };

            return View(command);
        }
    }
}
