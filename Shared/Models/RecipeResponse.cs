namespace Shared.Models
{
    internal class RecipeResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SourceURL { get; set; }
        public string Image { get; set; }
        public string Summary { get; set; }
        public string[] DishTypes { get; set; }
        public string SpoonacularSourceUrl { get; set; }
        public int UsedIngredientCount { get; set; }
        public IngredientResponse[] UsedIngredients { get; set; }
        public int MissedIngredientCount { get; set; }
        public IngredientResponse[] MissedIngredients { get; set; }
        public IngredientResponse[] UnusedIngredients { get; set; }
    }
}
