using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieList.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieList.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }


        public DbSet<Movie> Movies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>().HasData(
                    new Movie(){
                        Id = 1,
                        Title = "Spirited Away",
                        Description = "During her family's move to the suburbs," +
                        " a sullen 10-year-old girl wanders into a world ruled by" +
                        " gods, witches and spirits, a world where humans are changed" +
                        " into beasts.",
                        Director = "Hayao Miyazaki",
                        YearReleased = 2001,
                        CoverUrl = ""
                    },
                    new Movie()
                    {
                        Id = 2,
                        Title = "Pulp Fiction",
                        Description = "The lives of two mob hitmen, a boxer, a " +
                        "gangster and his wife, and a pair of diner bandits " +
                        "intertwine in four tales of violence and redemption.",
                        Director = "Quentin Tarantino",
                        YearReleased = 1994,
                        CoverUrl = ""
                    },
                    new Movie()
                    {
                        Id = 3,
                        Title = "Asteroid city",
                        Description = "The itinerary of a Junior Stargazer " +
                        "convention is spectacularly disrupted by world-changing" +
                        " events.",
                        Director = "Wes Anderson",
                        YearReleased = 2023,
                        CoverUrl = ""
                    }
                );
        }
    }
}
