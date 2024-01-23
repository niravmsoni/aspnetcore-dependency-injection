# Code
	- Dependency Injection - Decoupling interfaces and implementation of interface and class
	- If applications require same classes (For ex: IConfiguration to read settings), when we do not use DI, one may accidentally re-create the same class and pass a different instance to different service
	- This is the pain point that DI solves. In-case if more than 1 classes require a common dependency, with DI, we register it in the container and then WE CAN REUSE the same dependency from DI container
	- With larger solution, the number of dependencies will keep on increasing making code difficult to maintain

	- NuGet package
		- Install this package "Microsoft.Extensions.Hosting"
		- This package contains "Microsoft.Extensions.DependencyInjection" which is required for using DI in .NET Core
		- There are 2 phases with DI:
			- Registration Phase (Prior to Build)
				- Work with IServiceCollection class
				- We decide lifetime of the class and add it to IServiceCollection class
				- We register types in container so that it knows of their existence and when to construct them

			- Resolving Phase (Post Build)
				- Work with IServiceProvider class
				- We request for class from DI container
				- Container responsible for instantiating types and providing them when requested

		- With DI in place,
			- We are no longer dealing with object creation
			- DI container calls constructors
			- Not concerned with ordering of registrations 
			- No need to worry about dependencies of each type. We have to register types individually and then DI container does the heavy lifting of creating instance of classes in the correct order with required dependencies automatically
			- For ex: IProductSource is required as a dependency for IProductImporter service so, first DI container will resolve ProductSource and then ProductImporter

		- Well known DI containers
			- Autofac
			- Ninject
			- Unity (Discontinued)
			- Microsoft.Extensions.DependencyInjection - Default with .NET core

		- Registering Types
			serviceCollection.AddTransient<IProductSource, ProductSource>();
				- First parameter is service type (Mostly interface)
				- Second parameter is implementation type (Mostly an implementation of the interface)
			serviceCollection.AddTransient<ProductImporter>();
				- If service type is same as implementation type, we do not need an interface. We can register them directly

		- Resolving Types
			- host.Services.GetRequiredService<ProductImporter>();			
				- When resolving, we request instance of service type. Container finds implementation type, instantiates if needed and returns to us

		- Dependency Inversion
			- High level modules should not depend on low level modules. They should depend on abstractions

		- Inversion of Control
			- Framework controls which code is executed next and not our code
			- .NET Core has Program as startup. It runs first. And likewise, the framework decides what classes it needs to request from DI container for ensuring our application runs

		- Using DI container we are following both Dependency Inversion and Inversion of Control principle

		- Creating maintainable solutions using DI
			- Design classes with single responsibility
			- Depend upon abstractions (Mostly would be interfaces in C#) and not the classes
			- Interfaces are OWNED by consumer. They should describe capabilities required by consumer. How a particular implementation would be achieved should not be taken into consideration when designing interfaces
			- Have no assumptions about implementations