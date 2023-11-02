using Shared.Validation;
using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class IngredientList
    {
        [DuplicateIngredientValidator]
        public List<string> Ingredients { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string SelectedIngredient { get; set; }
    }
}
