using ElVegetarianoFurio.DataObjects;

namespace ElVegetarianoFurio.Store {
    public class EFDishRepository : IDishRepository {
        private readonly ApplicationContext _context;
        public EFDishRepository(ApplicationContext context) {
            _context = context;
        }

        public Dish CreateDish(Dish dish) {
            _context.Dishes.Add(dish);
            _context.SaveChanges();
            return dish;
        }

        public void DeleteDishById(int id) {
            var dish = _context.Dishes.Find(id);
            if (dish != null) {
                _context.Dishes.Remove(dish);
                _context.SaveChanges();
            }
        }

        public Dish GetDishById(int id) {
            return _context.Dishes.Find(id);
        }

        public IEnumerable<Dish> GetDishes() {
            return _context.Dishes;
        }

        public Dish UpdateDish(Dish dish) {
            var dishToUpdate = _context.Dishes.Find(dish.Id);
            dishToUpdate.Name = dish.Name;
            dishToUpdate.Description = dish.Description;
            dishToUpdate.Price = dish.Price;
            dishToUpdate.CategoryId = dish.CategoryId;
            _context.SaveChanges();
            return dishToUpdate;
        }
    }
}
