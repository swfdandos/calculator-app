
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace rEST_API3.Controllers;





[HttpPost]
public IActionResult MyEndpoint([FromBody] MyRequestModel request)
{
    // Verileri alın
    var data = request.Data;

    // Verileri işleyin
    var result = ProcessData(data);

    // Sonucu dönün
    return Ok(result);
}


[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository; // Ürün veritabanı işlemlerini gerçekleştiren bir repository

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        var products = _productRepository.GetProducts();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        var product = _productRepository.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> CreateProduct(Product product)
    {
        _productRepository.AddProduct(product);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, Product updatedProduct)
    {
        var existingProduct = _productRepository.GetProductById(id);
        if (existingProduct == null)
        {
            return NotFound();
        }
        existingProduct.Name = updatedProduct.Name;
        existingProduct.Price = updatedProduct.Price;
        _productRepository.UpdateProduct(existingProduct);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var existingProduct = _productRepository.GetProductById(id);
        if (existingProduct == null)
        {
            return NotFound();
        }
        _productRepository.DeleteProduct(existingProduct);
        return NoContent();
    }
}

