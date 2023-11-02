using Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace Shared.Validation
{
    /// <summary>
    /// Validates that <see cref="IngredientList.Ingredients"/> does not contain duplicate items.
    /// </summary>
    public class DuplicateIngredientValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            List<string>? ingredients = value as List<string>;
            IngredientList? ingredientList = validationContext?.ObjectInstance as IngredientList;
            ArgumentNullException.ThrowIfNull(ingredients);
            ArgumentNullException.ThrowIfNull(ingredientList);

            if (!ingredients.Contains(ingredientList.SelectedIngredient))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Ingredients cannot be duplicates.",
                new List<string> { validationContext.MemberName });
            }
        }
    }
}
