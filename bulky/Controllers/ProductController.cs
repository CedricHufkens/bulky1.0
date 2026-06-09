using bulky.Data;
using bulky.Models;
using bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;

namespace bulky.Controllers
{
    public class ProductController : Controller
    {
        // maakt het mogleijk om met EF gebruik te maken.
        private readonly ApplicationDbContext _db;
        //mogelijk maken om met afbeeldingen te werken.
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDbContext db,IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objproductList = _db.Producten.Include(p => p.Category).ToList();
            //De list met category objecten moeten we vanuit de controller doorgeven aan 
            //de view (index). in de view gaan we dan deze list opvangen.
            return View(objproductList);
        }
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> categorylist = _db.Categories.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            ProductVM productVM = new ProductVM();
            productVM.Product = new Product();
            productVM.CategoryList = categorylist;

            return View(productVM);
        }
        [HttpPost]
        public IActionResult Create(ProductVM obj, IFormFile? file)
        {
            
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file!= null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    using (var filestream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    obj.Product.ImageUrl = @"\images\product\" + fileName;
                }
                _db.Producten.Add(obj.Product);
                _db.SaveChanges();
                TempData["success"] = "Product created succesfully";
                return RedirectToAction("Index", "Product");
            }
            return View(obj);

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
            //Lijst van categories samenstellen om dropdownlist te vullen
            IEnumerable<SelectListItem> categoryList =
                _db.Categories.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

            //Werd er vanuit de view een ID mee gegeven? 
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //op basis van ID het juiste Category object uit _db ophalen
            Product? productFromDb = _db.Producten.Find(id);

            //product gevonden?
            if (productFromDb == null)
            {
                return NotFound();
            }

            ProductVM productVM = new ProductVM();

            productVM.Product = productFromDb;
            productVM.CategoryList = categoryList;

            return View(productVM);


        }
        [HttpPost]
        public IActionResult Edit(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(obj.Product.ImageUrl))
                    {
                        //oude afbeelding verwijderen, hiervoor hebben we het pad naar afbeelding nodig
                        //in database staat er in het begin een backslash, die moeten we verwijderen
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.Product.ImageUrl = @"\images\product\" + fileName;

                    _db.Producten.Update(obj.Product);
                    _db.SaveChanges();
                    TempData["success"] = "Product edited!";
                    //Je wilt een actie van de controller met de naam Index terug uitvoeren
                    //hiermee worden alle categories terug geladen en getoond en ga je terug naar deze pagina
                    return RedirectToAction("Index", "Product");
                }
            }
            return View(obj);
        }
    }

}

