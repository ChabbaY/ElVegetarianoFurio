using ElVegetarianoFurio.DataObjects;

namespace ElVegetarianoFurio.Store {
    public interface IDishRepository {
        IEnumerable<Dish> GetDishes();
        Dish GetDishById(int id);
        Dish CreateDish(Dish dish);
        Dish UpdateDish(Dish dish);
        void DeleteDishById(int id);
    }
}