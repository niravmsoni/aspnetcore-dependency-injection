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
			- Resolving Phase (Post Build)
				- Work with IServiceProvider class
				- We request for class from DI container

		- With DI in place,
			- We are no longer dealing with object creation
			- DI container calls constructors
			- Not concerned with ordering of registrations 
			- No need to worry about dependencies of each type. We have to register types individually and then DI container does the heavy lifting of creating instance of classes with required dependencies automatically