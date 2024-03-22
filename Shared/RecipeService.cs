using Serilog;
using Serilog.Core;
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

        //need a free api key from here: https://spoonacular.com/food-api
        private const string apiKey = "";

        public async Task<IEnumerable<Recipe>> GetRecipesAsync_Fake()
        {
            await Task.Delay(1000 * 2);

            string json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "SampleResponse.json"));

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            RecipeListResponse result = JsonSerializer.Deserialize<RecipeListResponse>(json, options);

            Log.Logger.Debug($"{nameof(GetRecipesAsync_Fake)}: Made a fake api call with sample ingredients potato, onion, leek (real ingredients were ignored)");
            Log.Logger.Debug($"{nameof(GetRecipesAsync_Fake)}: Received {result.Results.Count()} results.");

            return result.Results.Select(x => MapRecipe(x));
        }

        public async Task<(IEnumerable<Recipe> Recipes, double? ApiQuota)> GetRecipesAsync(List<string> ingredients)
        {
            if (ingredients?.Count == 0) return (Enumerable.Empty<Recipe>(), null);

            string ingredientList = string.Join(",", ingredients);
            string theUrl = $@"https://api.spoonacular.com/recipes/complexSearch?addRecipeInformation=true&fillIngredients=true&sort=max-used-ingredients&instructionsRequired=true&limitLicense=true&number={MaxRecipes}&apiKey={apiKey}&includeIngredients={ingredientList}";

            Log.Logger.Debug($"{nameof(GetRecipesAsync)}: Making http request: {theUrl}");

            using HttpRequestMessage message = new(HttpMethod.Get, new Uri(theUrl));

            HttpResponseMessage response = await _httpClient.SendAsync(message);

            response.EnsureSuccessStatusCode();
            RecipeListResponse result = await response.Content.ReadFromJsonAsync<RecipeListResponse>();
            double quotaLeft = GetQuotaFromHeader(response.Headers);

            Log.Logger.Debug($"{nameof(GetRecipesAsync)}: Made api call with ingredients: {string.Join(";", ingredients)}");
            Log.Logger.Debug($"{nameof(GetRecipesAsync)}: Recieved {result.Results.Count()} results.");
            Log.Logger.Debug($"{nameof(GetRecipesAsync)}: Quota left {quotaLeft}");

            List<Recipe> recipes = result.Results.OrderByDescending(x => x.UsedIngredientCount).ThenBy(x => x.MissedIngredientCount).ThenByDescending(x => x.AggregateLikes)
                .Select(x => MapRecipe(x)).ToList();
            return (recipes, quotaLeft);
        }

        public async Task<double> GetQuota()
        {
            string theUrl = $@"https://api.spoonacular.com/food/converse/suggest?query=tell&number=1&apiKey={apiKey}";

            Log.Logger.Debug($"{nameof(GetQuota)}: Making http request: {theUrl}");

            using HttpRequestMessage message = new(HttpMethod.Get, new Uri(theUrl));

            HttpResponseMessage response = await _httpClient.SendAsync(message);

            response.EnsureSuccessStatusCode();
            double quotaLeft = GetQuotaFromHeader(response.Headers);

            Log.Logger.Debug($"{nameof(GetQuota)}: Quota left {quotaLeft}");

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
