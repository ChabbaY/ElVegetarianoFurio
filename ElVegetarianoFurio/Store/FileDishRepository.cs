using ElVegetarianoFurio.DataObjects;
using Newtonsoft.Json;

namespace ElVegetarianoFurio.Store {
    public class FileDishRepository : IDishRepository {
        private readonly string _filePath;
        public FileDishRepository(IWebHostEnvironment env) {
            _filePath = Path.Combine(env.ContentRootPath, "Data", "dishes.json");
        }

        public Dish CreateDish(Dish dish) {
            dish.Id = 99;
            return dish;
        }

        public void DeleteDishById(int id) {
            
        }

        public Dish GetDishById(int id) {
            return GetDishes()?.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Dish> GetDishes() {
            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<IEnumerable<Dish>>(json);
        }

        public Dish UpdateDish(Dish dish) {
            return dish;
        }
    }
}