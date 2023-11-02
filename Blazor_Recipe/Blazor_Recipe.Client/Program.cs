using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Serilog;
using Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Log.Logger = HelperService.CreateLogger();

await builder.Build().RunAsync();
