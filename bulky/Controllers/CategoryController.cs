using bulky.Data;
using bulky.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace bulky.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        //dat is een acion method de naam van de action method bepaalt welke view gaat teruggeven
        public IActionResult Edit(int? id)
        {
            if (id == null || id ==0)
            {
                return NotFound();
            }
            Category categoryFromdb = _db.Categories.Find(id); //.Find werkt alkel oop primary key
            //Andere methode om dit te doen:
            Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Met First of default ook mofelijk om nop anderze velde dan de primary key te zoeken
            Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (categoryFromdb==null)
            {
                return NotFound();
            }
            return View(categoryFromdb);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromdb = _db.Categories.Find(id); 
            if (categoryFromdb == null)
            {
                return NotFound();
            }
            return View(categoryFromdb);
        }
        public IActionResult Index()
        {
            //_db= een object van de kasse applicationDbcontext
            //we gebruiken dit object om een List<Category> te creëren
            List<Category> objcategoryList = _db.Categories.ToList();
            //De list met category objecten moeten we vanuit de controller doorgeven aan 
            //de view (index). in de view gaan we dan deze list opvangen.
            return View(objcategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Order cannot be exactly match the name.");
            }
            if (obj.Name!=null && obj.Name.ToLower() =="test")
            {
                ModelState.AddModelError("", "Test is not a valid category name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created succesfully";
                return RedirectToAction("Index", "Category");
            }
            return View();

        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Edited succesfully";
                return RedirectToAction("Index", "Category");
            }
            return View();

        }
        [HttpPost, ActionName("delete")]
        public IActionResult DeletePost(int? id )
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted succesfully";
            return RedirectToAction("Index", "Category");
            

        }

    }
}
