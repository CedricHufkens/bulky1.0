using bulky.Data;
using bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace bulky.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Product> objproductList = _db.Producten.ToList();
            //De list met category objecten moeten we vanuit de controller doorgeven aan 
            //de view (index). in de view gaan we dan deze list opvangen.
            return View(objproductList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {

            if (ModelState.IsValid)
            {
                _db.Producten.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Product created succesfully";
                return RedirectToAction("Index", "Product");
            }
            return View();

        }
        [HttpPost, ActionName("delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? obj = _db.Producten.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Producten.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Product Deleted succesfully";
            return RedirectToAction("Index", "Product");


        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product ProductFromdb = _db.Producten.Find(id);
            if (ProductFromdb == null)
            {
                return NotFound();
            }
            return View(ProductFromdb);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product productFromdb = _db.Producten.Find(id); //.Find werkt alkel oop primary key
            //Andere methode om dit te doen:
            Product? productFromdb1 = _db.Producten.FirstOrDefault(u => u.Id == id);
            //Met First of default ook mofelijk om nop anderze velde dan de primary key te zoeken
            Product? productFromdb2 = _db.Producten.Where(u => u.Id == id).FirstOrDefault();
            if (productFromdb == null)
            {
                return NotFound();
            }
            return View(productFromdb);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {

            if (ModelState.IsValid)
            {
                _db.Producten.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Product Edited succesfully";
                return RedirectToAction("Index", "Product");
            }
            return View();

        }

    }
}
