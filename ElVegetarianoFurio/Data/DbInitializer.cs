using ElVegetarianoFurio.DataObjects;
using ElVegetarianoFurio.Store;
using Newtonsoft.Json;

namespace ElVegetarianoFurio.Data {
    public class DbInitializer {
        public static void Initialize(ApplicationContext context, IWebHostEnvironment env) {
            context.Database.EnsureCreated();
            if (context.Categories.Any() || context.Dishes.Any()) {
                return;
            }

            var path = Path.Combine(env.ContentRootPath, "Data");
            var dishesJson = File.ReadAllText(Path.Combine(path, "dishes.json"));
            var categoriesJson = File.ReadAllText(Path.Combine(path, "categories.json"));

            var dishes = JsonConvert.DeserializeObject<List<Dish>>(dishesJson);
            var categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJson);

            foreach (var category in categories) {
                foreach (var dish in dishes.Where(x => x.CategoryId == category.Id)) {
                    dish.Id = 0;
                    category.Dishes.Add(dish);
                }
                category.Id = 0;
                context.Categories.Add(category);
            }
            context.SaveChanges();
        }
    }
}