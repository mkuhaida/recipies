namespace Recipes.Entities
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public decimal DifficultyLevel { get; set; }
        public Section Section { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
