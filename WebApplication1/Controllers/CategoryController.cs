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
                _db.Categories.Add(obj); //Add the new category using EF core
                _db.SaveChanges(); //Save changes to the DB
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id==null || id ==0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id); //If entity with the given primary key is found, return, otherwise null is returned 
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id); //LINQ method "FoD" to find the first category that matches the given Id. 
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault(); 

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost] //Post the Added Category to DB
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid) //Go to category.cs and examine the validation
            {
                _db.Categories.Update(obj); //Update the category using EF Core
                _db.SaveChanges(); //Save changes to the DB
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id); //If entity with the given primary key is found, return, otherwise null is returned 

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")] 
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges(); //Save changes to the DB
            return RedirectToAction("Index");

        }
    }
}
