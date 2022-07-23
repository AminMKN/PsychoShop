using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PsychoShop.Application.Contracts.Inventory;
using PsychoShop.Application.Contracts.Product;
using PsychoShop.Framework.Application;
using ServiceHost.Areas.Admin.Models;

namespace ServiceHost.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    [Authorize(Policy = "InventoryPolicy")]
    public class InventoryController : Controller
    {
        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;

        public InventoryController(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _productApplication = productApplication;
            _inventoryApplication = inventoryApplication;
        }

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index(InventorySearchModel searchModel)
        {
            var command = new InventoryAdminCommand()
            {
                Products = new SelectList(await _productApplication.GetProductsList(), "Id", "Name"),
                Inventory = await _inventoryApplication.Search(searchModel)
            };

            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> OperationLog(int id)
        {
            var command = new InventoryAdminCommand()
            {
                InventoryOperations = await _inventoryApplication.GetOperationLog(id)
            };

            return View(command);
        }

        #endregion

        #region Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var command = new InventoryAdminCommand()
            {
                Products = new SelectList(await _productApplication.GetProductsList(), "Id", "Name")
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InventoryAdminCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = _inventoryApplication.Create(command.CreateInventory);
                command.Message = result.Message;
            }

            command.Products = new SelectList(await _productApplication.GetProductsList(), "Id", "Name");
            return View(command);
        }

        #endregion

        #region Edit

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var command = new InventoryAdminCommand()
            {
                Products = new SelectList(await _productApplication.GetProductsList(), "Id", "Name"),
                EditInventory = _inventoryApplication.GetDetails(id)
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InventoryAdminCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = _inventoryApplication.Edit(command.EditInventory);
                command.Message = result.Message;
            }

            command.Products = new SelectList(await _productApplication.GetProductsList(), "Id", "Name");
            return View(command);
        }

        #endregion

        #region Increase

        [HttpGet]
        public IActionResult Increase(int id)
        {
            var command = new InventoryAdminCommand()
            {
                IncreaseInventory = new IncreaseInventory()
                {
                    InventoryId = id
                }
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Increase(InventoryAdminCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = _inventoryApplication.Increase(command.IncreaseInventory);
                command.Message = result.Message;
            }

            return View(command);
        }

        #endregion

        #region Reduce

        [HttpGet]
        public IActionResult Reduce(int id)
        {
            var command = new InventoryAdminCommand()
            {
                ReduceInventory = new ReduceInventory()
                {
                    InventoryId = id
                }
            };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reduce(InventoryAdminCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = _inventoryApplication.Reduce(command.ReduceInventory);
                command.Message = result.Message;
            }

            return View(command);
        }

        #endregion
    }
}
