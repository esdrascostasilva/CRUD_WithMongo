using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CrudWithMongodb;

public class ProductService
{
    private readonly IMongoCollection<Product> _productCollection;

    public ProductService(IOptions<ProductDatabaseSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);

        _productCollection = mongoDatabase.GetCollection<Product>(options.Value.ProductCollectionName);
    }

}
