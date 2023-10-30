using FluentValidation;
using FluentValidation.Results;
using Shared.Models;

namespace Shared.Validators
{
    public class IngredientValidator : AbstractValidator<string>
    {
        public IngredientValidator()
        {
            RuleFor(x => x).NotEmpty();
        }

        public Func<string, Task<IEnumerable<string>>> Validation => ValidateIngredientList;

        private async Task<IEnumerable<string>> ValidateIngredientList(string ingredient)
        {
            ValidationResult result = await ValidateAsync(ingredient);

            if (result.IsValid)
            {
                return Array.Empty<string>();
            }
            return result.Errors.Select(e => e.ErrorMessage);
        }
    }
}
