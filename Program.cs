using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var users = new List<User>
{
    new User()
    {
        Id=1,
        FirstName ="Vedant",
        LastName = "Malaikar"
    },
    new User()
    {
        Id=2,
        FirstName = "Vishakha",
        LastName = "Malaikar"
    },
    new User()
    {
        Id=3,
        FirstName = "Shlok",
        LastName = "Malaikar"
    }
};

app.MapGet("/users", () =>
{
    return Results.Ok(users);
});

app.MapGet("/users/{id}", (int id) =>
{
    var user = users.Where(x => x.Id == id).FirstOrDefault();

    if (user is null) return Results.NotFound();

    return Results.Ok(user);
});

app.MapPost("/users", (User user) =>
{
    users.Add(user);

    return Results.Created($"/users/{user.Id}", user);
});

app.MapDelete("/users/{id}", (int id) =>
{
    var user = users.Where(x => x.Id == id).FirstOrDefault();

    if (user is null) return Results.NotFound();

    users.Remove(user);

    return Results.NoContent();
});

app.MapPut("/users/{id}", (User updateUser, int id) =>
{
    var user = users.Where(x => x.Id == id).FirstOrDefault();

    if (user is null) return Results.NotFound();

    user.FirstName = updateUser.FirstName;
    user.LastName = updateUser.LastName;

    return Results.Ok(user);
});

app.Run();

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}