using Microsoft.EntityFrameworkCore;
using Recipes.Entities;

namespace Recipes
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Recipe> Recipies { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .HasOne(b => b.User)
                .WithMany(a => a.Recipies)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>().HasData(
                    new User()
                    {
                        Id = new Guid("4B6A1054-4FE8-4CE1-B6AC-6180B1DA7095"),
                        FullName = "Nefedova Vasilisa Fedorovna",
                        Email = "nefedova.v@gmail.com",
                        Phone = "+375(29)783-56-05",
                        BirthDate = new DateTime(2000, 11, 12),
                        UserLevel = UserLevel.Beginner
                    },
                    new User()
                    {
                        Id = new Guid("9257793e-8cb7-11ee-a4a2-80e82c270b17"),
                        FullName = "Smirnova Alena Rostislavovna",
                        Email = "smirnova123456@mail.ru",
                        Phone = "+375(33)853-86-98",
                        BirthDate = new DateTime(2002, 07, 19),
                        UserLevel = UserLevel.Beginner
                    }
            );
            modelBuilder.Entity<Recipe>().HasData(
                    new Recipe()
                    {
                        Id = new Guid("03D5A75A-46CE-4C7F-9A12-F762F2331DC5"),
                        UserId = new Guid("4B6A1054-4FE8-4CE1-B6AC-6180B1DA7095"),
                        Name = "Baked Potato Soup",
                        Description = "You'll find the full, step-by-step recipe below — but here's a brief overview of what you can expect when you make baked potato soup at home:  Cook the bacon.  Melt the butter, then whisk in the flour and milk.  Add the potatoes and onions.",
                        DifficultyLevel = 3.3m,
                        Section = Section.Desserts,
                        CreatedOn = DateTime.Now
                    }
            );
        }
    }
}
