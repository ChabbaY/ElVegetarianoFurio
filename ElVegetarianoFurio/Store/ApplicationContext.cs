using ElVegetarianoFurio.DataObjects;
using Microsoft.EntityFrameworkCore;

namespace ElVegetarianoFurio.Store {
    public class ApplicationContext : DbContext {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //additional settings for certain entity options
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Dish> Dishes { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}