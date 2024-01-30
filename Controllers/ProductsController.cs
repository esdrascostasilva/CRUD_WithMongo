using Microsoft.AspNetCore.Mvc;

namespace CrudWithMongodb;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : Controller
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<List<Product>> GetAllProducts()
    {
        return await _productService.GetAll();
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Product>> GetProduct(string id)
    {
        var product = await _productService.GetById(id);

        if (product == null)
            return BadRequest();

        return product;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(Product productRequest)
    {
        await _productService.Create(productRequest);
        return CreatedAtAction(nameof(GetProduct), new { id = productRequest.Id }, productRequest);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateProduct(string id, Product productRequest)
    {
        var product = await _productService.GetById(id);

        if (product == null)
            return BadRequest();

        await _productService.Update(id, productRequest);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> RemoveProduct(string id)
    {
        var product = _productService.GetById(id);

        if (product == null)
            return BadRequest();

        await _productService.Delete(id);

        return NoContent();
    }
}
