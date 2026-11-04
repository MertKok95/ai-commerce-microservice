using Catalog.API.Models;
using Catalog.API.DTOs;
using MongoDB.Driver;
using Mapster;

var builder = WebApplication.CreateBuilder(args);

var mongoSettings = builder.Configuration.GetSection("MongoDbSettings");
var client = new MongoClient(mongoSettings["ConnectionString"]);
var database = client.GetDatabase(mongoSettings["DatabaseName"]);
var productsCollection = database.GetCollection<Product>(mongoSettings["CollectionName"]);

builder.Services.AddSingleton(productsCollection);

var app = builder.Build();

if (productsCollection.CountDocuments(FilterDefinition<Product>.Empty) == 0)
{
    productsCollection.InsertMany(new[]
    {
        new Product { Name = "Quantum X1", Description = "Noise-cancelling wireless headphones", Price = 1999.99m, Category = "Electronics" },
        new Product { Name = "ErgoMesh Chair", Description = "Orthopedic office chair", Price = 4500.00m, Category = "Furniture" }
    });
}

app.MapGet("/api/products", async (IMongoCollection<Product> collection) =>
{
    var products = await collection.Find(_ => true).ToListAsync();
    var productDtos = products.Adapt<List<ProductDto>>();
    
    return Results.Ok(productDtos);
});

app.Run();