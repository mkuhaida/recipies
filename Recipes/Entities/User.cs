namespace Recipes.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public UserLevel UserLevel { get; set; }
        public ICollection<Recipe> Recipies { get; set; }
    }
}
