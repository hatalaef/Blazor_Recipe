using FluentValidation;
using FluentValidation.Results;
using Shared.Models;

namespace Shared.Validators
{
    public class IngredientListValidator : AbstractValidator<IngredientList>
    {
        public IngredientListValidator()
        {
            RuleFor(x => x).NotEmpty();
            RuleForEach(x => x.Ingredients).SetValidator(new IngredientValidator());
        }

        public Func<IngredientList, Task<IEnumerable<string>>> Validation => ValidateIngredientList;

        private async Task<IEnumerable<string>> ValidateIngredientList(IngredientList ingredients)
        {
            ValidationResult result = await ValidateAsync(ingredients);

            if (result.IsValid)
            {
                return Array.Empty<string>();
            }
            return result.Errors.Select(e => e.ErrorMessage);
        }
    }
}
