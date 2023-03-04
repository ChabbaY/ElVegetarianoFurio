using ElVegetarianoFurio.DataObjects;
using ElVegetarianoFurio.Store;
using Microsoft.AspNetCore.Mvc;

namespace ElVegetarianoFurio.Controllers {
    [Route("api/[controller]")]
    public class CategoriesController : Controller {
        private readonly ICategoryRepository _repository;
        public CategoriesController(ICategoryRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Category> Get() {
            return _repository.GetCategories();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            var category = _repository.GetCategoryById(id);
            if (category == null) {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Category category) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var result = _repository.CreateCategory(category);
            return CreatedAtAction("Get", new { id = category.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category category) {
            if (id != category.Id) {
                return BadRequest();
            }
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            if (_repository.GetCategoryById(id) == null) {
                return NotFound();
            }

            var result = _repository.CreateCategory(category);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            if (_repository.GetCategoryById(id) == null) {
                return NotFound();
            }
            _repository.DeleteCategoryById(id);
            return NoContent();
        }
    }
}