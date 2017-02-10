using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfzoo.entities.validator
{
    //Source : http://stackoverflow.com/questions/34924687/c-sharp-entity-framework-data-validation-between-add-to-context-and-savechanges/34939218#34939218

    public static class EntityValidator
    {
        public static void Validate<T>(T entity)
        {
            // uses the extension method GetValidationErrors defined below
            if (entity.GetValidationErrors().Any())
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

        public static string GetValidationErrorMessages(this object obj)
        {
            var error = "";

            var errors = obj.GetValidationErrors();
            var validationResults = errors as ValidationResult[] ?? errors.ToArray();
            if (!validationResults.Any())
            {
                return error;
            }

            foreach (var ee in validationResults)
            {
                foreach (var n in ee.MemberNames)
                {
                    error += ee + "; ";
                }
            }

            return error;
        }
    }
}
