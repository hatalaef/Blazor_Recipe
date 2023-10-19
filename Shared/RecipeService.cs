using Shared.Models;

namespace Shared
{
    public class RecipeService
    {
        private const string apiKey = "c71c3bdaa5e645a5b0b283e0a0d6ed57";
        private string url = $@"https://api.spoonacular.com/recipes/complexSearch?apiKey={apiKey}&includeIngredients=milk&addRecipeInformation=true";

        private static readonly List<RecipeResponse> _responses =
        [
            new RecipeResponse
            {
                Id = 715393,
                Title = "Berry-licious Smoothie Bowl",
                SourceURL = "http://www.pinkwhen.com/berry-licious-smoothie-bowl-recipe/",
                Image = "https://spoonacular.com/recipeImages/715393-312x231.jpg",
                Summary = "Berry-licious Smoothie Bowl takes around <b>45 minutes</b> from beginning to end. This recipe serves 1 and costs $3.16 per serving. One portion of this dish contains about <b>19g of protein</b>, <b>12g of fat</b>, and a total of <b>304 calories</b>. Head to the store and pick up almond, kiwi, milk, and a few other things to make it today. A couple people made this recipe, and 57 would say it hit the spot. It is brought to you by Pink When. It is a good option if you're following a <b>gluten free, lacto ovo vegetarian, and primal</b> diet. It works well as a pretty expensive main course. With a spoonacular <b>score of 90%</b>, this dish is excellent. Users who liked this recipe also liked <a href=\"https://spoonacular.com/recipes/berry-licious-smoothie-bowl-1405581\">Berry-licious Smoothie Bowl</a>, <a href=\"https://spoonacular.com/recipes/very-berry-licious-smoothies-401754\">Very Berry-licious Smoothies</a>, and <a href=\"https://spoonacular.com/recipes/mixed-berry-smoothie-bowl-668858\">Mixed Berry Smoothie Bowl</a>.",
                DishTypes = [
                    "lunch",
                    "main course",
                    "main dish",
                    "dinner"
                ],
                SpoonacularSourceUrl = "https://spoonacular.com/berry-licious-smoothie-bowl-715393",
                UsedIngredientCount = 2,
                MissedIngredientCount = 4,
                MissedIngredients =
                [
                    new IngredientResponse
                    {
                    }
                ]
            },
            new RecipeResponse
            {
                Id = 661740,
                Title = "Strawberry and Banana Lassi",
                SourceURL = "http://www.foodista.com/recipe/WLM4QM8T/strawberry-and-banana-lassi",
                Image = "https://spoonacular.com/recipeImages/661740-312x231.jpg",
                Summary = "Strawberry and Banana Lassi is an Indian beverage. One serving contains <b>312 calories</b>, <b>16g of protein</b>, and <b>2g of fat</b>. This gluten free and lacto ovo vegetarian recipe serves 1 and costs <b>$3.64 per serving</b>. Not a lot of people made this recipe, and 1 would say it hit the spot. It can be enjoyed any time, but it is especially good for <b>Mother's Day</b>. It is brought to you by Foodista. From preparation to the plate, this recipe takes roughly <b>5 minutes</b>. Head to the store and pick up non-fat milk, non-fat greek yogurt, curry powder, and a few other things to make it today. Taking all factors into account, this recipe <b>earns a spoonacular score of 79%</b>, which is pretty good. If you like this recipe, take a look at these similar recipes: <a href=\"https://spoonacular.com/recipes/lassi-sweet-punjabi-lassi-how-to-make-lassi-488422\">lassi , sweet punjabi lassi | how to make lassi</a>, <a href=\"https://spoonacular.com/recipes/strawberry-lassi-152609\">Strawberry Lassi</a>, and <a href=\"https://spoonacular.com/recipes/strawberry-lassi-488185\">Strawberry Lassi</a>.",
                DishTypes = [
                    "beverage",
                    "drink"
                ],
                SpoonacularSourceUrl = "https://spoonacular.com/strawberry-and-banana-lassi-661740",
                UsedIngredientCount = 2,
                MissedIngredientCount = 4,
                MissedIngredients =
                [
                    new IngredientResponse
                    {
                    }
                ]
            },
            new RecipeResponse
            {
                Id = 643241,
                Title = "Four-Berry Blast Fruit Smoothie",
                SourceURL = "http://www.foodista.com/recipe/445N2G3Y/four-berry-blast-fruit-smoothie",
                Image = "https://spoonacular.com/recipeImages/643241-312x231.jpg",
                Summary = "If you want to add more <b>gluten free and lacto ovo vegetarian</b> recipes to your recipe box, Four-Berry Blast Fruit Smoothie might be a recipe you should try. This recipe serves 4. This breakfast has <b>261 calories</b>, <b>12g of protein</b>, and <b>11g of fat</b> per serving. For <b>$1.24 per serving</b>, this recipe <b>covers 15%</b> of your daily requirements of vitamins and minerals. 6 people were impressed by this recipe. Head to the store and pick up milk, blueberries, vanillan extract, and a few other things to make it today. From preparation to the plate, this recipe takes about <b>10 minutes</b>. It is brought to you by Foodista. Overall, this recipe earns a <b>solid spoonacular score of 74%</b>. Try <a href=\"https://spoonacular.com/recipes/four-berry-blast-fruit-smoothie-1364149\">Four-Berry Blast Fruit Smoothie</a>, <a href=\"https://spoonacular.com/recipes/ginger-berry-fruit-smoothie-1434023\">Ginger Berry Fruit Smoothie</a>, and <a href=\"https://spoonacular.com/recipes/ginger-berry-fruit-smoothie-1253437\">Ginger Berry Fruit Smoothie</a> for similar recipes.",
                DishTypes = [
                    "morning meal",
                    "brunch",
                    "beverage",
                    "breakfast",
                    "drink"
                ],
                SpoonacularSourceUrl = "https://spoonacular.com/four-berry-blast-fruit-smoothie-643241",
                UsedIngredientCount = 2,
                MissedIngredientCount = 4,
                MissedIngredients =
                [
                    new IngredientResponse
                    {
                    }
                ]
            }
        ];

        public async Task<IEnumerable<Recipe>> GetRecipesAsync(List<string> ingredients)
        {
            return await Task.FromResult(_responses.Select(x => new Recipe
            {
                Id = x.Id,
                Name = x.Title,
                Summary = x.Summary,
                Url = x.SpoonacularSourceUrl
            }).ToList());
        }
    }
}
