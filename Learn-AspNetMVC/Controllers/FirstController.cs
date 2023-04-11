using Learn_AspNetMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
		public IActionResult ViewProduct(int? id)
		{
			var product = _productService.Where(p=>p.Id == id).FirstOrDefault();
			if(product== null)
			{
				return NotFound();
			}
			//return View(product);
			this.ViewData["product"] =product;
			return View("ViewProduct2");
		}
	}
}
