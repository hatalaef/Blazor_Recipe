using Serilog;
using Serilog.Core;

namespace Shared
{
    public static class HelperService
    {
        public static string GetImageUrl(int recipeId)
        {
            return $"https://spoonacular.com/recipeImages/{recipeId}-636x393.jpg";
        }

        public static Logger CreateLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.BrowserConsole() //for the client
                .WriteTo.Console() //for the server
                .CreateLogger();
        }
    }
}
