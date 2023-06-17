using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieList.Models.Models
{
    public class ApplicationUser : IdentityUser
    {
        public required string Name { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set;}
    }
}
