# Code
	- Dependency Injection - Decoupling interfaces and implementation of interface and class
	- If applications require same classes (For ex: IConfiguration to read settings), when we do not use DI, one may accidentally re-create the same class and pass a different instance to different service
	- This is the pain point that DI solves. In-case if more than 1 classes require a common dependency, with DI, we register it in the container and then WE CAN REUSE the same dependency from DI container
	- With larger solution, the number of dependencies will keep on increasing making code difficult to maintain