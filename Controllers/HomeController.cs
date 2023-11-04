using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieApp.Data;
using MovieApp.Models;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;

namespace MovieApp.Controllers
{
    public class HomeController : Controller
    {
        
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(int? id, string q)
        {
            var movies = MovieRepository.Movies;
            if(id != null)
            {   
                movies= movies.Where(i=>i.CategoryId == id).ToList();
            }
            if (!string.IsNullOrEmpty(q))
            {
                movies = movies.Where(a => a.Name.ToLower().Contains(q.ToLower())).ToList();
            }
            return View(movies);
         }
         public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(CategoryRepository.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
         public IActionResult Create(Movie _movie)
        {
            if (ModelState.IsValid)
            {
                MovieRepository.AddMovie(_movie);
                return RedirectToAction("index");
            }
            return View(_movie);
        }
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Details(int id)
            
        {
           
            return View(MovieRepository.GetById(id));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
