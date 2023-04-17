using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Learn_AspNetMVC.Models;
using Learn_AspNetMVC.Models.Contacts;
using Microsoft.VisualStudio.Web.CodeGeneration.CommandLine;
using Microsoft.AspNetCore.Authorization;

namespace Learn_AspNetMVC.Areas.Contact.Controllers
{
    [Area("Contact")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        [TempData]
        public string StatusMessage { get; set; }
        
        [HttpGet("/admin/contact")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contacts.ToListAsync());
        }

        [HttpGet("/admin/contact/details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactModel = await _context.Contacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactModel == null)
            {
                return NotFound();
            }

            return View(contactModel);
        }

        // GET: Contact/Create
        [HttpGet("/contact/")]
        [AllowAnonymous]
        public IActionResult SendContact()
        {
            return View();
        }


        [HttpPost("/contact/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendContact([Bind("FullName,Email,Message,Phone")] ContactModel contactModel)
        {
            contactModel.DateSent= DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(contactModel);
                await _context.SaveChangesAsync();
                StatusMessage = "Lien he cua ban da duoc gui";
                return RedirectToAction("Index","Home");
            }
            return View(contactModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactModel = await _context.Contacts.FindAsync(id);
            if (contactModel == null)
            {
                return NotFound();
            }
            return View(contactModel);
        }

        [HttpGet("/admin/contact/delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactModel = await _context.Contacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactModel == null)
            {
                return NotFound();
            }

            return View(contactModel);
        }

        [HttpPost("/admin/contact/delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactModel = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contactModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
