using MovieList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MovieList.Utility
{
    public class AllowedDirectorNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            
            var director = value as string;
            
            if (director != null)
            {
                if (!director.Trim().Any(c => Char.IsWhiteSpace(c)))
                {
                    return new ValidationResult($"The name must be formatted with a space between the first and last names.");
                }
                return ValidationResult.Success;
            }
            return new ValidationResult("Director name is required.");
        }
    }
}
