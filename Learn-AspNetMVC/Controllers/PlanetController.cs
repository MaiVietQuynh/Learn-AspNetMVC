using Learn_AspNetMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Learn_AspNetMVC.Controllers
{
    public class PlanetController : Controller
    {
        private readonly PlanetService _planetService;
        private readonly ILogger<PlanetController> _logger;
        public PlanetController(PlanetService planetService, ILogger<PlanetController> logger)
        {
            _planetService = planetService;
            _logger = logger;
        }

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
		public IActionResult Saturn()
		{
			var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
			return View("Detail", planet);
		}
		public IActionResult PlanetInfo(int id)
		{
			var planet = _planetService.Where(p => p.Id == id).FirstOrDefault();
			return View("Detail", planet);
		}

	}
}
