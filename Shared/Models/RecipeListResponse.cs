namespace Shared.Models
{
    internal class RecipeListResponse
    {
        public RecipeResponse[] Results { get; set; }
        public int Offset { get; set; }
        public int Number { get; set; }
        public int TotalResults { get; set; }
    }
}
