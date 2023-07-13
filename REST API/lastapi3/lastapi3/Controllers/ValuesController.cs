using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lastapi3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // Değerleri döndürme
            return Ok(new string[] { "Value1", "Value2" });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // Belirli bir değeri döndürme
            var value = $"Value {id}";
            return Ok(value);
        }

        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            // Yeni bir değer ekleyin
            // Veritabanına kaydetme vb. işlemleri burada yapabilirsiniz
            return CreatedAtAction(nameof(Get), new { id = 1 }, value);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            // Değerin güncellenmesi
            // Veritabanında güncelleme vb. işlemleri 
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Değeri silme
            // Veritabanından silme vb. işlemleri 
            return NoContent();
        }
    }
}
