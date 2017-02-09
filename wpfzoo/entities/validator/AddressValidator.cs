using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfzoo.entities.validator
{
    public static class AddressValidator
    {
        public static void Validate(Address address)
        {
            // uses the extension method GetValidationErrors defined below
            if (address.GetValidationErrors().Any())
            {
                throw new ValidationException();
            }
        }
    }

    public static class ValidationHelpers
    {
        public static IEnumerable<ValidationResult> GetValidationErrors(this object obj)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(obj, null, null);
            Validator.TryValidateObject(obj, context, validationResults, true);
            return validationResults;
        }
    }

}
