using ElVegetarianoFurio.DataObjects;
using Microsoft.EntityFrameworkCore;

namespace ElVegetarianoFurio.Store {
    public class EFCategoryRepository : ICategoryRepository {
        private readonly ApplicationContext _context;
        public EFCategoryRepository(ApplicationContext context) {
            _context = context;
        }

        public Category CreateCategory(Category category) {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public void DeleteCategoryById(int id) {
            var category = _context.Categories.Find(id);
            if (category != null) {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public Category GetCategoryById(int id) {
            return _context.Categories.Find(id);
        }

        public IEnumerable<Category> GetCategories() {
            return _context.Categories.Include(x => x.Dishes);
        }

        public Category UpdateCategory(Category category) {
            var categoryToUpdate = _context.Categories.Find(category.Id);
            categoryToUpdate.Name = category.Name;
            categoryToUpdate.Description = category.Description;
            _context.SaveChanges();
            return categoryToUpdate;
        }
    }
}
