using Learn_AspNetMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Learn_AspNetMVC.Controllers
{
	public class FirstController : Controller
	{
		private readonly ILogger<FirstController> _logger;
		private readonly ProductService _productService;
		public FirstController(ILogger<FirstController> logger,ProductService productService)
		{
			_logger= logger;
			_productService= productService;
		}
		public IActionResult Index()
		{
			return View();
		}
		[TempData]
		public string StatusMessage { get; set; }

        public IActionResult ViewProduct(int? id)
		{
			var product = _productService.Where(p=>p.Id == id).FirstOrDefault();
			if(product== null)
			{
				//TempData["StatusMessage"] = "San pham ban yeu cau khong co";
				StatusMessage = "San pham ban yeu cau khong co";
                return Redirect(Url.Action("Index", "Home"));
			}
			//return View(product);
			//-ViewData
			//this.ViewData["product"] =product;
			//return View("ViewProduct2");
			//-ViewBag
			ViewBag.product = product;
			return View("ViewProduct3");
		}


    }
}
