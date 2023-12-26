using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NLog;
using Recipes;
using Recipes.Entities;

/*List<User> users = new List<User>
{
    new()
    { 
        Id = new Guid("4B6A1054-4FE8-4CE1-B6AC-6180B1DA7095"),
        FullName = "Nefedova Vasilisa Fedorovna",
        Email = "nefedova.v@gmail.com",
        Phone = "+375(29)783-56-05",
        BirthDate = new DateTime(2000,11,12),
        UserLevel = UserLevel.Beginner
    },
    new()
    {
        Id = new Guid("56604012-9BDE-4D3E-BB86-0004576AB537"),
        FullName = "aaaa",
        Email = "segfsgsv@gmail.com",
        Phone = "+375(29)783-56-05",
        BirthDate = new DateTime(2000,11,13),
        UserLevel = UserLevel.Beginner
    }
};

List<Recipe> recipes = new List<Recipe>
{
    new()
    {
        Id = new Guid("03D5A75A-46CE-4C7F-9A12-F762F2331DC5"),
        UserId = users[0].Id,
        User = users[0],
        Name = "Baked Potato Soup",
        Description = "You'll find the full, step-by-step recipe below Ч but here's a brief overview of what you can expect when you make baked potato soup at home:  Cook the bacon.  Melt the butter, then whisk in the flour and milk.  Add the potatoes and onions.",
        DifficultyLevel = 3.3m,
        Section = Section.Desserts,
        CreatedOn = DateTime.Now
    }
};*/


UserDto ToUserDto(User user) =>
    new UserDto()
    {
        Id = user.Id,
        FullName = user.FullName,
        Email = user.Email,
        Phone = user.Phone,
        BirthDate = user.BirthDate,
        UserLevel = user.UserLevel.ToString()
    };

RecipeDto ToRecipeDto(Recipe recipe) =>
    new RecipeDto()
    {
        Id = recipe.Id,
        UserId = recipe.UserId,
        UserName = recipe.User?.FullName,
        Name = recipe.Name,
        Description = recipe.Description,
        DifficultyLevel = recipe.DifficultyLevel,
        Section = recipe.Section.ToString(),
        CreatedOn = recipe.CreatedOn
    };

var logger = CreateLogger();

var builder = WebApplication.CreateBuilder();
try
{
    logger.Info("Starting migrations execution...");

    var connection = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));
}
catch (Exception e)
{
    logger.Info(e, "An error occurred while executing the command");
}


var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/users", async (ApplicationContext db) =>
{
    logger.Info("GetUsers");
    return (await db.Users.ToListAsync()).Select(ToUserDto);
});

app.MapGet("/api/recipes", async (ApplicationContext db) =>
{
    logger.Info("GetRecipes");
    return (await db.Recipies.Include(x => x.User).ToListAsync()).Select(x => ToRecipeDto(x));
 });

app.MapGet("/api/users/{id}", async(ApplicationContext db, string id) =>
{
    // получаем пользовател€ по id
    Guid.TryParse(id, out var guidId);
    User? user = await db.Users.FirstOrDefaultAsync(u => u.Id == guidId);
    // если не найден, отправл€ем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "ѕользователь не найден" });

    // если пользователь найден, отправл€ем его
    return Results.Json(ToUserDto(user));
});

app.MapGet("/api/recipes/{id}", async (ApplicationContext db, string id) =>
{
    // получаем пользовател€ по id
    Guid.TryParse(id, out var guidId);
    Recipe? recipe = await db.Recipies.Include(x => x.User).FirstOrDefaultAsync(u => u.Id == guidId);
    // если не найден, отправл€ем статусный код и сообщение об ошибке
    if (recipe == null) return Results.NotFound(new { message = "–ецепт не найден" });

    // если пользователь найден, отправл€ем его
    return Results.Json(ToRecipeDto(recipe));
});

app.MapDelete("/api/users/{id}", async (ApplicationContext db, string id) =>
{
    // получаем пользовател€ по id
    Guid.TryParse(id, out var guidId);
    User? user = await db.Users.FirstOrDefaultAsync(u => u.Id == guidId);

    // если не найден, отправл€ем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "ѕользователь не найден" });

    // если пользователь найден, удал€ем его
    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Results.Json(ToUserDto(user));
});

app.MapDelete("/api/recipes/{id}", async (ApplicationContext db, string id) =>
{
    // получаем пользовател€ по id
    Guid.TryParse(id, out var guidId);
    Recipe? recipe = await db.Recipies.FirstOrDefaultAsync(u => u.Id == guidId);

    // если не найден, отправл€ем статусный код и сообщение об ошибке
    if (recipe == null) return Results.NotFound(new { message = "–ецепт не найден" });

    // если пользователь найден, удал€ем его
    db.Recipies.Remove(recipe);
    await db.SaveChangesAsync();
    return Results.Json(ToRecipeDto(recipe));
});

app.MapPost("/api/users", async(ApplicationContext db, UserDto user) => {

    // устанавливаем id дл€ нового пользовател€
    // добавл€ем пользовател€ в список
    db.Users.Add(new User()
    {
        Id = Guid.NewGuid(),
        FullName = user.FullName,
        Email = user.Email,
        Phone = user.Phone,
        BirthDate = user.BirthDate,
        UserLevel = Enum.Parse<UserLevel>(user.UserLevel)
    });
    await db.SaveChangesAsync();

    return Results.Json(user);
});

app.MapPost("/api/recipes/{userId}", async(ApplicationContext db, RecipeDto recipe, string userId) => {

    Guid.TryParse(userId, out var guidId);
    User? user = await db.Users.FirstOrDefaultAsync(u => u.Id == guidId);
    // если не найден, отправл€ем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "ѕользователь не найден" });

    // устанавливаем id дл€ нового пользовател€
    // добавл€ем пользовател€ в список
    db.Recipies.Add(new Recipe()
    {
        Id = Guid.NewGuid(),
        UserId = user.Id,
        User = user,
        Name = recipe.Name,
        Description = recipe.Description,
        DifficultyLevel = recipe.DifficultyLevel,
        Section = Enum.Parse<Section>(recipe.Section),
        CreatedOn = DateTime.UtcNow
    });
    await db.SaveChangesAsync();

    return Results.Json(recipe);
});

app.MapPut("/api/users", async (ApplicationContext db, UserDto userData) => {

    // получаем пользовател€ по id
    var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userData.Id);
    // если не найден, отправл€ем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "ѕользователь не найден" });
    // если пользователь найден, измен€ем его данные и отправл€ем обратно клиенту

    user.FullName = userData.FullName;
    user.Email = userData.Email;
    user.Phone = userData.Phone;
    user.BirthDate = userData.BirthDate;
    user.UserLevel = Enum.Parse<UserLevel>(userData.UserLevel);

    db.Users.Update(user);
    await db.SaveChangesAsync();

    return Results.Json(ToUserDto(user));
});

app.MapPut("/api/recipes", async (ApplicationContext db, RecipeDto recipeData) => {

    // получаем пользовател€ по id
    var recipe = await db.Recipies.FirstOrDefaultAsync(u => u.Id == recipeData.Id);
    // если не найден, отправл€ем статусный код и сообщение об ошибке
    if (recipe == null) return Results.NotFound(new { message = "–ецепт не найден" });
    // если пользователь найден, измен€ем его данные и отправл€ем обратно клиенту

    recipe.Name = recipeData.Name;
    recipe.Description = recipeData.Description;
    recipe.DifficultyLevel = recipeData.DifficultyLevel;
    recipe.Section = Enum.Parse<Section>(recipeData.Section);

    db.Recipies.Update(recipe);
    await db.SaveChangesAsync();

    return Results.Json(ToRecipeDto(recipe));
});

app.Run();


Logger CreateLogger()
{
    var configuration = new NLog.Config.LoggingConfiguration();
    var logConsole = new NLog.Targets.ConsoleTarget("logconsole");
    configuration.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logConsole);

    LogManager.Configuration = configuration;

    return LogManager.GetCurrentClassLogger();
};

public class UserDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime BirthDate { get; set; }
    public string UserLevel { get; set; }
};

public class RecipeDto
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string? UserName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal DifficultyLevel { get; set; }
    public string Section { get; set; }
    public DateTime CreatedOn { get; set; }
};

