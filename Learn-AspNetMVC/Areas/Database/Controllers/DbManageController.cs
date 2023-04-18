using Learn_AspNetMVC.Data;
using Learn_AspNetMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_AspNetMVC.Areas.Database.Controllers
{
    [Area("Database")]
    [Route("/database-manage/[action]")]
    public class DbManageController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManage;
        public DbManageController(AppDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManage)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManage = roleManage;
        }

        public IActionResult Index()
		{
			return View();
		}
        [HttpGet]
        public IActionResult DeleteDb()
        {
            return View();
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpPost]
        public async Task<IActionResult> DeleteDbAsync()
        {
            var success = await _dbContext.Database.EnsureDeletedAsync();
            StatusMessage = success ? "Xóa Database thành công" : "Không xóa được Db";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Migrate()
        {
            await _dbContext.Database.MigrateAsync();
            StatusMessage = "Cap nhat Database thành công" ;
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> SeedDataAsync()
        {
            var roleNames = typeof(RoleName).GetFields().ToList();
            foreach (var r in roleNames)
            {
                var rolename = (string)r.GetRawConstantValue();
                var rfound = await _roleManage.FindByNameAsync(rolename);
                if(rfound == null)
                {
                    await _roleManage.CreateAsync(new IdentityRole(rolename));
                }
                //admin admin123 admin@example.com
                var userAdmin = await _userManager.FindByNameAsync("admin");
                if(userAdmin== null)
                {
                    userAdmin = new AppUser()
                    {
                        UserName = "admin",
                        Email = "admin@example.com",
                        EmailConfirmed = true
                    };
                    await _userManager.CreateAsync(userAdmin, "admin123");
                    await _userManager.AddToRoleAsync(userAdmin, RoleName.Administrator);
                }
            }
            StatusMessage = "Ban vua Seed Database";
            return RedirectToAction("Index");
        }
        
    }
}
