namespace CrudWithMongodb;

public class ProductDatabaseSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string ProductCollectionName { get; set; } // As if it were the SQL table
}
