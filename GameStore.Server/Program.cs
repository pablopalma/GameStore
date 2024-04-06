using GameStore.Server.Data;
using GameStore.Server.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Configure CORS policies during service registration
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5203")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var connString = builder.Configuration.GetConnectionString("GameStoreContext");
builder.Services.AddSqlServer<GameStorecontext>(connString);

var app = builder.Build();
// Apply CORS policies during request processing
app.UseCors();

var group = app.MapGroup("/games");
group.WithParameterValidation();

// Get /games
group.MapGet("/", async (string? filter, GameStorecontext context) =>
{
    var games = context.Games.AsNoTracking();

    if(filter is not null)
    {
        games = games.Where(games => games.Name.Contains(filter) || games.Genre.Contains(filter));
    }

    return await games.ToListAsync();
});

// Get /games/{id}
group.MapGet("/{id}", async (int id, GameStorecontext context) =>
{
    Game? game = await context.Games.FindAsync(id);

    if (game is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(game);
})
.WithName("GetName");

// Post /games
group.Map("/", async (Game game, GameStorecontext context) =>
{
    context.Games.Add(game);
    await context.SaveChangesAsync();
    return Results.CreatedAtRoute("GetName", new {id = game.Id}, game);
}).WithParameterValidation();

// PUT /games/{id}
group.Map("/{id}", async (int id, Game updatedGame, GameStorecontext context) =>
{
    var rowsAffected = await context.Games.Where(game => game.Id == id)
    .ExecuteUpdateAsync(updates => updates
    .SetProperty(game => game.Name, updatedGame.Name)
    .SetProperty(game => game.Genre, updatedGame.Genre)
    .SetProperty(game => game.Price, updatedGame.Price)
    .SetProperty(game => game.ReleaseDate, updatedGame.ReleaseDate));

    return rowsAffected == 0 ? Results.NotFound() : Results.NoContent();
});

// DELETE games/{id}
group.MapDelete("/{id}", async (int id, GameStorecontext context) =>
{
    var rowsAffected = await context.Games.Where(game => game.Id == id)
    .ExecuteDeleteAsync();
    return rowsAffected == 0 ? Results.NotFound() : Results.NoContent();
});

app.Run();
