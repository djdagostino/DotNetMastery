using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db; 
        }
        public IActionResult Index()
        {
            var objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();  
        }
        [HttpPost] //Post the Added Category to DB
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid) //Go to category.cs and examine the validation
            {
                _db.Categories.Add(obj); //Add the object 
                _db.SaveChanges(); //Save changes to the DB
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
