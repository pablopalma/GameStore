using GameStore.Server.Data;
using GameStore.Server.Data.Repositories.Implementation;
using GameStore.Server.Data.Repositories.Interfaces;
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

// Register the repository
builder.Services.AddScoped<GameRepository>();

var app = builder.Build();
// Apply CORS policies during request processing
app.UseCors();

var group = app.MapGroup("/games");
group.WithParameterValidation();

// Get /games
//group.MapGet("/", async (string? filter, GameStorecontext context) =>
//{
//    var games = context.Games.AsNoTracking();

//    if(filter is not null)
//    {
//        games = games.Where(games => games.Name.Contains(filter) || games.Genre.Contains(filter));
//    }

//    return await games.ToListAsync();
//});

// Inject the repository into route handlers
group.MapGet("/", async (string? filter, GameRepository gameRepository) =>
{
    return await gameRepository.GetGamesAsync(filter);
});

// Get /games/{id}
group.MapGet("/{id}", async (int id, GameRepository gameRepository) =>
{
    var game = await gameRepository.GetGameByIdAsync(id);
    return game != null ? Results.Ok(game) : Results.NotFound();
})
.WithName("GetName");

// Post /games
group.Map("/", async (Game game, GameRepository gameRepository) =>
{
    var gameId = await gameRepository.AddGameAsync(game);
    return Results.CreatedAtRoute("GetName", new { id = gameId }, game);
}).WithParameterValidation();

// PUT /games/{id}
group.Map("/{id}", async (int id, Game updatedGame, GameRepository gameRepository) =>
{
    var result = await gameRepository.UpdateGameAsync(id, updatedGame);
    return result ? Results.NoContent() : Results.NotFound();
});

// DELETE games/{id}
group.MapDelete("/{id}", async (int id, GameRepository gameRepository) =>
{
    var result = await gameRepository.DeleteGameAsync(id);
    return result ? Results.NoContent() : Results.NotFound();
});

app.Run();
