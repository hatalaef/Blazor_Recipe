namespace Shared.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }

        public List<string> UsedIngredients { get; set; } = new List<string>();
        public List<string> MissedIngredients { get; set; } = new List<string>();
        public List<string> UnusedIngredients { get; set; } = new List<string>();
    }
}
