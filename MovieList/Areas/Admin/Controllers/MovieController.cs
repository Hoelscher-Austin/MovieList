using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieList.DataAccess.Data;
using MovieList.Models.Models;
using MovieList.Utility;
using System.Text.RegularExpressions;

namespace MovieList.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MovieController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = context;
            _webHostEnvironment = webHostEnvironment;
        }
        //GET Actions
        public IActionResult Index()
        {
            List<Movie> moviesList = _dbContext.Movies.ToList();
            return View(moviesList);
        }

        public IActionResult Add()
        {
            return View();
        }


        
        public IActionResult Edit(int? id)
        {

            if(id == null || id == 0) {
                return NotFound();
            }

            Movie? movie = _dbContext.Movies.FirstOrDefault(x => x.Id == id);

            return View(movie);
        }



        //POST Actions

        [HttpPost]
        public IActionResult Edit(Movie movie, IFormFile? file)
        {


           
            if(ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                   

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string moviePath = Path.Combine(wwwRootPath, @"images\Movie");

                    if (!string.IsNullOrEmpty(movie.CoverUrl))
                    {
                        var oldPath = Path.Combine(wwwRootPath, movie.CoverUrl.TrimStart('\\'));

                        if(System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(moviePath, fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    movie.CoverUrl = @"\images\Movie\" + fileName;
                }

                movie.Director = movie.Director.Trim();
                movie.Director = Regex.Replace(movie.Director, @"\s+", " ");
                movie.Description = Regex.Replace(movie.Description, @"<p>|</p>","");
                _dbContext.Movies.Update(movie);
                _dbContext.SaveChanges();
                TempData["success"] = "Movie Updated Successfully";
                return RedirectToAction("Index");

            }
            return View();
        }
        [HttpPost]
        public IActionResult Add(Movie movie, IFormFile? file)
        {
            if (ModelState.IsValid)
            {

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {


                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string moviePath = Path.Combine(wwwRootPath, @"images\Movie");

         

                    using (var fileStream = new FileStream(Path.Combine(moviePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    movie.CoverUrl = @"\images\Movie\" + fileName;
                }


                movie.Director = movie.Director.Trim();
                movie.Director = Regex.Replace(movie.Director, @"\s+", " ");
                movie.Description = Regex.Replace(movie.Description, @"<p>|</p>", "");
                _dbContext.Movies.Add(movie);
                _dbContext.SaveChanges();
                TempData["success"] = "Movie Added Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpDelete]
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            Movie? movie = _dbContext.Movies.FirstOrDefault(m => m.Id == id);
            if(movie == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(movie.CoverUrl))
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var oldPath = Path.Combine(wwwRootPath, movie.CoverUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
            }

            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();
            TempData["success"] = "Movie Deleted Successfully";
            return RedirectToAction("Index");
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll() { 
            List<Movie> movieList = _dbContext.Movies.ToList();
            return Json(new {data = movieList});
        }

        #endregion








    }
}
