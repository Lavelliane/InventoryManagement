using InventoryManagementWeb.Data;
using InventoryManagementWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User obj)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Login(User obj)
        {
            var myUser = _db.Users
         .FirstOrDefault(u => u.Email == obj.Email
                      && u.Password == obj.Password);
            if (myUser == null)
            {
                return View("Index");
            }
            else if(myUser.Email.Equals("admin@admin.com"))
            {
                return RedirectToAction("Admin", "Category");
            }
            
            return RedirectToAction("Index", "Category");
        }
    }
}
