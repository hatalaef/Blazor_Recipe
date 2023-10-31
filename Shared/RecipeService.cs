using Shared.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Shared
{
    public class RecipeService
    {
        private readonly HttpClient _httpClient;

        private const int MaxRecipes = 15;

        public RecipeService(HttpClient client)
        {
            _httpClient = client;
        }

        private const string apiKey = "c71c3bdaa5e645a5b0b283e0a0d6ed57";

        public async Task<IEnumerable<Recipe>> GetRecipesAsync_Fake(List<string> ingredients)
        {
            await Task.Delay(1000 * 2);

            string json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "SampleResponse.json"));

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            RecipeListResponse result = JsonSerializer.Deserialize<RecipeListResponse>(json, options);

            Console.WriteLine($"{nameof(GetRecipesAsync_Fake)}: Made a fake api call with sample ingredients potato, onion, leek (real ingredients were ignored)");
            Console.WriteLine($"{nameof(GetRecipesAsync_Fake)}: Received {result.Results.Count()} results.");

            return result.Results.Select(x => MapRecipe(x));
        }

        public async Task<(IEnumerable<Recipe> Recipes, double? ApiQuota)> GetRecipesAsync(List<string> ingredients)
        {
            if (ingredients?.Count == 0) return (Enumerable.Empty<Recipe>(), null);

            string ingredientList = string.Join(",", ingredients);
            string theUrl = $@"https://api.spoonacular.com/recipes/complexSearch?addRecipeInformation=true&fillIngredients=true&sort=max-used-ingredients&instructionsRequired=true&limitLicense=true&number={MaxRecipes}&apiKey={apiKey}&includeIngredients={ingredientList}";

            Console.WriteLine($"{nameof(GetRecipesAsync)}: Making http request: {theUrl}");

            using HttpRequestMessage message = new(HttpMethod.Get, new Uri(theUrl));

            HttpResponseMessage response = await _httpClient.SendAsync(message);

            response.EnsureSuccessStatusCode();
            RecipeListResponse result = await response.Content.ReadFromJsonAsync<RecipeListResponse>();
            double quotaLeft = GetQuotaFromHeader(response.Headers);

            Console.WriteLine($"{nameof(GetRecipesAsync)}: Made api call with ingredients: {string.Join(";", ingredients)}");
            Console.WriteLine($"{nameof(GetRecipesAsync)}: Recieved {result.Results.Count()} results.");
            Console.WriteLine($"{nameof(GetRecipesAsync)}: Quota left {quotaLeft}");

            List<Recipe> recipes = result.Results.OrderByDescending(x => x.UsedIngredientCount).ThenBy(x => x.MissedIngredientCount).ThenByDescending(x => x.AggregateLikes)
                .Select(x => MapRecipe(x)).ToList();
            return (recipes, quotaLeft);
        }

        public async Task<double> GetQuota()
        {
            string theUrl = $@"https://api.spoonacular.com/food/converse/suggest?query=tell&number=1&apiKey={apiKey}";

            Console.WriteLine($"{nameof(GetQuota)}: Making http request: {theUrl}");

            using HttpRequestMessage message = new(HttpMethod.Get, new Uri(theUrl));

            HttpResponseMessage response = await _httpClient.SendAsync(message);

            response.EnsureSuccessStatusCode();
            double quotaLeft = GetQuotaFromHeader(response.Headers);

            Console.WriteLine($"{nameof(GetQuota)}: Quota left {quotaLeft}");

            return quotaLeft;
        }

        private static Recipe MapRecipe(RecipeResponse response)
        {
            return new Recipe
            {
                Id = response.Id,
                Name = response.Title,
                Summary = response.Summary,
                Url = response.SpoonacularSourceUrl,
                ImageUrl = HelperService.GetImageUrl(response.Id),
                MissedIngredients = response.MissedIngredients.Select(x => MapRecipeName(x)).ToList(),
                UnusedIngredients = response.UnusedIngredients.Select(x => MapRecipeName(x)).ToList(),
                UsedIngredients = response.UsedIngredients.Select(x => MapRecipeName(x)).ToList(),
            };
        }

        private static string MapRecipeName(IngredientResponse response)
        {
            return response.Name;
        }

        private static double GetQuotaFromHeader(HttpResponseHeaders headers)
        {
            return double.Parse(headers.GetValues("X-API-Quota-Left").Single());
        }
    }
}
