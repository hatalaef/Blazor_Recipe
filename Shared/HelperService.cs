namespace Shared
{
    public static class HelperService
    {
        public static string GetImageUrl(int recipeId)
        {
            return $"https://spoonacular.com/recipeImages/{recipeId}-636x393.jpg";
        }
    }
}
