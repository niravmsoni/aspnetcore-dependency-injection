using DependencyInjection;
using DependencyInjection.Shared;
using DependencyInjection.Source;
using DependencyInjection.Target;
using DependencyInjection.Transformation.Transformations;
using DependencyInjection.Transformation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DependencyInjection.Util;
using DependencyInjection.Target.EntityFramework;
using Microsoft.EntityFrameworkCore;

// Creating default builder for application host
using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        //Add service to IServiceCollection
        services.AddSingleton<Configuration>();

        //Whenever IPriceParser is used as dependency, we would get PriceParser from DI container at runtime
        services.AddTransient<IPriceParser, PriceParser>();
        services.AddTransient<IProductSource, ProductSource>();

        services.AddTransient<IProductFormatter, ProductFormatter>();
        services.AddTransient<IProductTarget, SqlProductTarget>();

        services.AddTransient<ProductImporter>();

        //Since this is registered as transient, we are getting new instance every-time we request this(ProductSource, ProductTarget and ProductImporter)
        //So in-essence there are 3 instances that get created for this svc.
        //services.AddTransient<IImportStatistics, ImportStatistics>();
        services.AddScoped<IImportStatistics, ImportStatistics>();

        services.AddTransient<IProductTransformer, ProductTransformer>();
        services.AddScoped<IProductTransformationContext, ProductTransformationContext>();
        services.AddScoped<INameDecapitaliser, NameDecapitaliser>();
        services.AddScoped<ICurrencyNormalizer, CurrencyNormalizer>();

        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IReferenceAdder, ReferenceAdder>();
        //Changing Top level service to be Scoped
        services.AddScoped<IReferenceGenerator, ReferenceGenerator>();

        //Child class service to be singleton
        services.AddSingleton<IIncrementingCounter, IncrementingCounter>();
        //Prior to build method, code is in registration Phase (Deals with IServiceCollection)

        //Registers all EFCore types
        services.AddDbContext<TargetContext>(options =>
        options.UseSqlServer(context.Configuration["TargetContextConnectionString"]));

    })
    .Build();

//Here it's in resolving phase and code deals with class IServiceProvider
var productImporter = host.Services.GetRequiredService<ProductImporter>();

#region Testing instance creation using DI
//using var firstScope = host.Services.CreateScope();
//var resolvedOnce = firstScope.ServiceProvider.GetRequiredService<ProductImporter>();
//var resolvedTwice = firstScope.ServiceProvider.GetRequiredService<ProductImporter>();

////True
//var isSameInFirstScope = Object.ReferenceEquals(resolvedTwice, resolvedOnce);

//using var secondScope = host.Services.CreateScope();
//var resolvedThrice = secondScope.ServiceProvider.GetRequiredService<ProductImporter>();
//var resolvedFourth = secondScope.ServiceProvider.GetRequiredService<ProductImporter>();

////True
//var isSameInSecondScope = Object.ReferenceEquals(resolvedThrice, resolvedFourth);

////False - Since we're comparing cross scope
//var IsSameInCrossScope = Object.ReferenceEquals(resolvedOnce, resolvedFourth);

//Console.WriteLine(isSameInFirstScope);
//Console.WriteLine(isSameInSecondScope);
//Console.WriteLine(IsSameInCrossScope);
#endregion

productImporter.Run();