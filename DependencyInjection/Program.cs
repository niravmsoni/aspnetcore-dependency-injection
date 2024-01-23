using DependencyInjection;
using DependencyInjection.Shared;
using DependencyInjection.Source;
using DependencyInjection.Target;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddTransient<Configuration>();
    })
    .Build();

var productImporter = host.Services.GetRequiredService<ProductImporter>();
productImporter.Run();

//Creating configuration object once and passing it to multiple classes that require this as a dependency.
//Works for smaller solutions but would be difficult for larger solutions
var configuration = new Configuration();

var priceParser = new PriceParser();
var productSource = new ProductSource(configuration, priceParser);

var productFormatter = new ProductFormatter();
var productTarget = new ProductTarget(configuration, productFormatter);

