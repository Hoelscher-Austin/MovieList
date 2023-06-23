using Microsoft.AspNetCore.Mvc;
using MovieList.DataAccess.Data;
using MovieList.Models;
using MovieList.Models.Models;
using System.Diagnostics;

namespace MovieList.Areas.Users.Controllers
{
    [Area("Users")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Movie> moviesList = _dbContext.Movies.ToList();
            return View(moviesList);
        }

        public IActionResult Details(int id)
        {
            Movie movie = _dbContext.Movies.FirstOrDefault(x => x.Id == id);
            return View(movie);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}