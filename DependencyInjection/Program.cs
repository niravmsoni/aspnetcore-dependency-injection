﻿using DependencyInjection;
using DependencyInjection.Shared;
using DependencyInjection.Source;
using DependencyInjection.Target;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Creating default builder for application host
using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        //Add service to IServiceCollection
        services.AddTransient<Configuration>();

        //Whenever IPriceParser is used as dependency, we would get PriceParser from DI container at runtime
        services.AddTransient<IPriceParser, PriceParser>();
        services.AddTransient<IProductSource, ProductSource>();

        services.AddTransient<IProductFormatter, ProductFormatter>();
        services.AddTransient<IProductTarget, ProductTarget>();

        services.AddTransient<ProductImporter>();

        //Since this is registered as transient, we are getting new instance every-time we request this(ProductSource, ProductTarget and ProductImporter)
        //So in-essence there are 3 instances that get created for this svc.
        //services.AddTransient<IImportStatistics, ImportStatistics>();
        services.AddScoped<IImportStatistics, ImportStatistics>();

        //Prior to build method, code is in registration Phase (Deals with IServiceCollection)
    })
    .Build();

//Here it's in resolving phase and code deals with class IServiceProvider
var productImporter = host.Services.GetRequiredService<ProductImporter>();
productImporter.Run();