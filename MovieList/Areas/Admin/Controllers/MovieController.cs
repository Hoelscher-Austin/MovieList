﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieList.DataAccess.Data;
using MovieList.Models.Models;

namespace MovieList.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public MovieController(ApplicationDbContext context)
        {
            _dbContext = context;
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

            if (id == null || id == 0)
            {
                return NotFound();
            }

            return View(movie);
        }



        //POST Actions

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            if(ModelState.IsValid)
            {
                _dbContext.Movies.Update(movie);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Add(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Movies.Add(movie);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
