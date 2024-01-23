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


var configuration = new Configuration();

var priceParser = new PriceParser();
var productSource = new ProductSource(configuration, priceParser);

var productFormatter = new ProductFormatter();
var productTarget = new ProductTarget(configuration, productFormatter);

