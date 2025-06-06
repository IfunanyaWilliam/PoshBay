using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PoshBay.Data.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {

        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extendions)
        {
                _extensions = extendions;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if(!_extensions.Contains(extension))
                {
                    return new ValidationResult($"The image extension is not allowed!");
                }
            }
            return ValidationResult.Success;
        }
    }
}
