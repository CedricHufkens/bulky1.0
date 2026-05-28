using bulky.Data;
using bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace bulky.Controllers
{
    public class BoekenController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BoekenController(ApplicationDbContext db)
        {
            _db = db;
        }
        //dat is een acion method de naam van de action method bepaalt welke view gaat teruggeven
        public IActionResult Index()
        {
            //_db= een object van de kasse applicationDbcontext
            //we gebruiken dit object om een List<Category> te creëren
            List<Boeken> objBoekenList = _db.Boeken.ToList();
            //De list met category objecten moeten we vanuit de controller doorgeven aan 
            //de view (index). in de view gaan we dan deze list opvangen.
            return View(objBoekenList); 

        }
    }
}
