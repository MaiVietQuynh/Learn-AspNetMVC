using Learn_AspNetMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Learn_AspNetMVC.Controllers
{
	[Route("he-mat-troi[action]")]
    public class PlanetController : Controller
    {
        private readonly PlanetService _planetService;
        private readonly ILogger<PlanetController> _logger;
        public PlanetController(PlanetService planetService, ILogger<PlanetController> logger)
        {
            _planetService = planetService;
            _logger = logger;
        }
		[Route("danh-sach-cac-hanh-tinh.html")]
		public IActionResult Index()
        {
            return View();
        }
        [BindProperty(SupportsGet =true, Name="action")]

        public string Name { get; set; }
        public IActionResult Mercury()
        {
            var planet = _planetService.Where(p=>p.Name== Name).FirstOrDefault();
            return View("Detail",planet);
        }
		public IActionResult Jupiter()
		{
			var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
			return View("Detail", planet);
		}
		public IActionResult Mars()
		{
			var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
			return View("Detail", planet);
		}
		public IActionResult Earth()
		{
			var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
			return View("Detail", planet);
		}
		public IActionResult Neptune()
		{
			var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
			return View("Detail", planet);
		}
		public IActionResult Uranus()
		{
			var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
			return View("Detail", planet);
		}
		public IActionResult Venus()
		{
			var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
			return View("Detail", planet);
		}
		[Route("sao/[action]",Order =1,Name ="Saturn1")] // ../sao/Saturn
		[Route("sao/[controller]/[action]", Order = 2)] // ../sao/Planet/Saturn
		[Route("[controller]-[action].html", Order = 3)] // ../sao/Saturn
		public IActionResult Saturn()
		{
			var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
			return View("Detail", planet);
		}
		[Route("hanhtinh/{id:int}")]
		public IActionResult PlanetInfo(int id)
		{
			var planet = _planetService.Where(p => p.Id == id).FirstOrDefault();
			return View("Detail", planet);
		}

	}
}
