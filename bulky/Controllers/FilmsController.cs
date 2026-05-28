using bulky.Data;
using bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace bulky.Controllers
{
    public class FilmsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public FilmsController(ApplicationDbContext db)
        {
            _db = db;
        }
       
    }
}
