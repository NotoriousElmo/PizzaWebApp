## EF

~~~bash
dotnet tool update --global dotnet-ef

dotnet ef migrations add --project DAL --startup-project WebApp InitialCreate
~~~

## WebPage

~~~bash
dotnet tool install --global dotnet-aspnet-codegenerator

cd WebApp
dotnet aspnet-codegenerator razorpage -m Domain.AdditionalComponent -outDir Pages/AdditionalComponents -dc AppDbContext -udl --referenceScriptLibraries 
dotnet aspnet-codegenerator razorpage -m Domain.AdditionalComponentInPizza -outDir Pages/AdditionalComponentsInPizzas -dc AppDbContext -udl --referenceScriptLibraries     
dotnet aspnet-codegenerator razorpage -m Domain.Order -outDir Pages/Orders -dc AppDbContext -udl --referenceScriptLibraries
dotnet aspnet-codegenerator razorpage -m Domain.Pizza -outDir Pages/Pizza -dc AppDbContext -udl --referenceScriptLibraries 
dotnet aspnet-codegenerator razorpage -m Domain.PizzaInOrder -outDir Pages/PizzasInOrders -dc AppDbContext -udl --referenceScriptLibraries 


~~~
