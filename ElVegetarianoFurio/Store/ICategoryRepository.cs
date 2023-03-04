using ElVegetarianoFurio.DataObjects;

namespace ElVegetarianoFurio.Store {
    public interface ICategoryRepository {
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(int id);
        Category CreateCategory(Category dish);
        Category UpdateCategory(Category dish);
        void DeleteCategoryById(int id);
    }
}