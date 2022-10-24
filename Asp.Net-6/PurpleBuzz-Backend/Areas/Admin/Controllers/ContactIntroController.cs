using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurpleBuzz_Backend.Areas.Admin.ViewModels;
using PurpleBuzz_Backend.DAL;
using PurpleBuzz_Backend.Models;

namespace PurpleBuzz_Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactIntroController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ContactIntroController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ContactIntroIndexViewModel
            {
                ContactIntroComponents = await _appDbContext.ContactIntroComponent.ToListAsync()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactIntroComponent contactIntroComponent)
        {
            if (!ModelState.IsValid) return View(contactIntroComponent);
            bool isExist = await _appDbContext.ContactIntroComponent.AnyAsync(c => c.Title.ToLower().Trim() == contactIntroComponent.Title.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("Title", "This title is already exist");
                return View(contactIntroComponent);
            }

            await _appDbContext.ContactIntroComponent.AddAsync(contactIntroComponent);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var contactIntroComponent =await _appDbContext.ContactIntroComponent.FindAsync(id);
            if (contactIntroComponent == null) return NotFound();
            return View(contactIntroComponent);
        }
    }
}
