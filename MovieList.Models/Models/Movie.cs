using MovieList.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MovieList.Models.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        [AllowedDirectorName]
        public string Director { get; set; }
        [Required]
        [DisplayName("Year Released")]
        [Range(1902,3000,ErrorMessage = "The Year must be between 1902-3000")]
        public int YearReleased { get; set; }
        
        public string? CoverUrl { get; set; }
        

    }
}
