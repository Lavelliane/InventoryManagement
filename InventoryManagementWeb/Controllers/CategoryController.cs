using InventoryManagementWeb.Data;
using InventoryManagementWeb.Models;
using InventoryManagementWeb.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementWeb.Controllers
{
  
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoryController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }
        //GET
        public IActionResult Create()
        {
            IEnumerable<Item> objItemList = _db.Items;
            return View(objItemList);
        }
        public IActionResult Admin()
        {

            IEnumerable<User> objUserList = _db.Users;
            return View(objUserList);
        }
        public IActionResult Confirm(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var itemFromDb = _db.Items.Find(id);
            if (itemFromDb == null)
            {
                return NotFound();

            }

            var obj = new Category
            {
                Name = itemFromDb.Name,
                Description = itemFromDb.Description,
                imgURL= itemFromDb.imgURL,
                ImageName = "xyz",
                price = itemFromDb.price,
                DisplayOrder = itemFromDb.DisplayOrder,
                CreatedDateTime = itemFromDb.CreatedDateTime,

            };
            _db.Categories.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name","The Display Order cannot be an exact match with the Item Name");
            }
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = obj.Name,
                    imgURL = obj.imgURL,
                    Description = obj.Description,
                    price = obj.price,
                    DisplayOrder = obj.DisplayOrder,
                    CreatedDateTime = DateTime.Now,

                };
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        private string UploadFile(CategoryViewModel obj)
        {
            string fileName = null;
            if(obj.ImageName != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + obj.ImageName.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    obj.ImageName.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        //GET (SHOW ON UPDATE VIEW)
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            if(categoryFromDb == null)
            {
                return NotFound();

            }

            var obj = new Category
            {
                Name = categoryFromDb.Name,
                Description = categoryFromDb.Description,
                price = categoryFromDb.price,
                DisplayOrder = categoryFromDb.DisplayOrder,
                CreatedDateTime = categoryFromDb.CreatedDateTime,
                ImageName = categoryFromDb.ImageName,
                imgURL = categoryFromDb.imgURL,

            };
            return View(obj);
        }
        //POST THE UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj, int id)
        {
            
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot be an exact match with the Item Name");
            }
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Id = id,
                    Name = obj.Name,
                    ImageName = "zxdsdf",
                    imgURL=obj.imgURL,
                    Description = obj.Description,
                    price = obj.price,
                    DisplayOrder = obj.DisplayOrder,
                    CreatedDateTime = DateTime.Now,

                };
                _db.Categories.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();

            }
            return View(categoryFromDb);
        }
        //POST THE UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            
       
        }
        public IActionResult EditAdmin(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var userFromDb = _db.Users.Find(id);
            if (userFromDb == null)
            {
                return NotFound();

            }

            var obj = new User
            {
                Email = userFromDb.Email,
                Password = userFromDb.Password,

            };
            return View(obj);
        }
        [HttpPost]
        public IActionResult EditUser(User obj, int id)
        {
          
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Id = id,
                    Email = obj.Email,
                    Password = obj.Password,
                   
                };
                _db.Users.Update(user);
                _db.SaveChanges();
                return RedirectToAction("Admin", "Category");
            }
            return View(obj);
        }
        public IActionResult DeleteAdmin(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var userFromDb = _db.Users.Find(id);
            if (userFromDb == null)
            {
                return NotFound();

            }
            return View(userFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteUser(int? id)
        {
            var obj = _db.Users.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Users.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Admin");


        }
        public IActionResult Payment()
        {
            return View();
        }
        public IActionResult  ClearRecord()
        {
            var categories = _db.Categories.ToList();

            foreach (var x in categories)
            {
                _db.Categories.Remove(x);
            }

            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
