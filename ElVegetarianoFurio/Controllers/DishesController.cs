using ElVegetarianoFurio.DataObjects;
using ElVegetarianoFurio.Store;
using Microsoft.AspNetCore.Mvc;

namespace ElVegetarianoFurio.Controllers {
    [Route("api/[controller]")]
    public class DishesController : Controller {
        private readonly IDishRepository _repository;
        public DishesController(IDishRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Dish> Get() {
            return _repository.GetDishes();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            var dish = _repository.GetDishById(id);
            if (dish == null) {
                return NotFound();
            }
            return Ok(dish);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Dish dish) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var result = _repository.CreateDish(dish);
            return CreatedAtAction("Get", new { id = dish.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Dish dish) {
            if (id != dish.Id) {
                return BadRequest();
            }
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            if (_repository.GetDishById(id) == null) {
                return NotFound();
            }

            var result = _repository.UpdateDish(dish);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            if (_repository.GetDishById(id) == null) {
                return NotFound();
            }
            _repository.DeleteDishById(id);
            return NoContent();
        }
    }
}