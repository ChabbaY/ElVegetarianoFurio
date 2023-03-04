using System.ComponentModel.DataAnnotations;

namespace ElVegetarianoFurio.DataObjects {
    public class Category {
        public Category() {
            Dishes = new HashSet<Dish>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; }
    }
}