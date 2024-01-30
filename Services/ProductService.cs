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

    public async Task<List<Product>> GetAll()
    {
        return await _productCollection.Find(x => true).ToListAsync();
    }

    public async Task<Product> GetById(string id)
    {
        return await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task Create(Product productRequest)
    {
        await _productCollection.InsertOneAsync(productRequest);
        return;
    }

    public async Task Update(string id, Product productRequest)
    {
        await _productCollection.ReplaceOneAsync(x => x.Id == id, productRequest);
        return;
    }

    public async Task Delete(string id)
    {
        await _productCollection.DeleteOneAsync(x => x.Id == id);
        return;
    }

}
