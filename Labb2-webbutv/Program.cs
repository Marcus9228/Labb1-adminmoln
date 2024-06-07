using Microsoft.EntityFrameworkCore;
using Labb2_webbutv.DB;
using Labb2_webbutv.Models;

namespace Labb2_webbutv
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<PokemonsDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddAuthorization();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:3000") // Replace with your Express.js app URL
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Apply migrations and seed the database
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<PokemonsDBContext>();
                dbContext.Database.Migrate();
                SeedDatabase(dbContext);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER")))
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Conditionally disable HTTPS redirection if running inside Docker
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER")))
            {
                app.UseHttpsRedirection();
            }

            app.UseCors("AllowSpecificOrigin");
            app.UseAuthorization();

            // POKEMONS
            app.MapGet("/pokemons", async (PokemonsDBContext dbContext) =>
            {
                try
                {
                    var pokemons = dbContext.Pokemons.ToList();
                    return Results.Ok(pokemons);
                }
                catch
                {
                    return Results.Problem("No pokemons in database");
                }
            });

            app.MapGet("/pokemon/{id}", async (PokemonsDBContext dbContext, int id) =>
            {
                try
                {
                    var pokemon = dbContext.Pokemons.ToList().Find(x => x.id == id);
                    return Results.Ok(pokemon);
                }
                catch
                {
                    return Results.Problem("No pokemon with this ID");
                }
            });

            app.MapPost("/pokemon", async (PokemonsDBContext dbContext, Pokemon pokemon) =>
            {
                try
                {
                    var newPokemon = new Pokemon()
                    {
                        PokemonID = pokemon.PokemonID,
                        Name = pokemon.Name,
                        Experience = pokemon.Experience,
                        Type = pokemon.Type
                    };
                    dbContext.Pokemons.Add(newPokemon);
                    await dbContext.SaveChangesAsync();
                    return Results.Ok(newPokemon);
                }
                catch
                {
                    return Results.Problem("No pokemon with this ID");
                }
            });

            app.MapPut("/pokemon/{id}", async (PokemonsDBContext dbContext, int id, Pokemon pokemon) =>
            {
                try
                {
                    var pokemonToUpdate = dbContext.Pokemons.ToList().Find(x => x.id == id);
                    pokemonToUpdate.Name = pokemon.Name;
                    pokemonToUpdate.Type = pokemon.Type;
                    pokemonToUpdate.Experience = pokemon.Experience;
                    pokemonToUpdate.PokemonID = pokemon.PokemonID;

                    dbContext.SaveChanges();
                    return Results.Ok(pokemonToUpdate);
                }
                catch
                {
                    return Results.Problem("No pokemon with this ID");
                }
            });

            app.MapDelete("/pokemon/{id}", async (PokemonsDBContext dbContext, int id) =>
            {
                try
                {
                    var pokemonToDelete = dbContext.Pokemons.ToList().Find(x => x.id == id);
                    dbContext.Pokemons.Remove(pokemonToDelete);

                    dbContext.SaveChanges();
                    return Results.Ok(pokemonToDelete);
                }
                catch
                {
                    return Results.Problem("No pokemon with this ID");
                }
            });

            // ITEMS
            app.MapGet("/items", async (PokemonsDBContext dbContext) =>
            {
                try
                {
                    var items = dbContext.Items.ToList();
                    return Results.Ok(items);
                }
                catch
                {
                    return Results.Problem("No items in database");
                }
            });

            app.MapGet("/item/{id}", async (PokemonsDBContext dbContext, int id) =>
            {
                try
                {
                    var item = dbContext.Items.ToList().Find(x => x.id == id);
                    return Results.Ok(item);
                }
                catch
                {
                    return Results.Problem("No item with this ID");
                }
            });

            app.MapPost("/item", async (PokemonsDBContext dbContext, Item item) =>
            {
                try
                {
                    var newItem = new Item()
                    {
                        Name = item.Name,
                        Description = item.Description,
                    };
                    dbContext.Items.Add(newItem);
                    await dbContext.SaveChangesAsync();
                    return Results.Ok(newItem);
                }
                catch
                {
                    return Results.Problem("No item with this ID");
                }
            });

            app.MapPut("/item/{id}", async (PokemonsDBContext dbContext, int id, Item item) =>
            {
                try
                {
                    var itemToUpdate = dbContext.Items.ToList().Find(x => x.id == id);
                    itemToUpdate.Name = item.Name;
                    itemToUpdate.Description = item.Description;

                    await dbContext.SaveChangesAsync();
                    return Results.Ok(itemToUpdate);
                }
                catch
                {
                    return Results.Problem("No item with this ID");
                }
            });

            app.MapDelete("/item/{id}", async (PokemonsDBContext dbContext, int id) =>
            {
                try
                {
                    var itemToDelete = dbContext.Items.ToList().Find(x => x.id == id);
                    dbContext.Items.Remove(itemToDelete);

                    await dbContext.SaveChangesAsync();
                    return Results.Ok(itemToDelete);
                }
                catch
                {
                    return Results.Problem("No item with this ID");
                }
            });

            // Set the URL to listen on
            app.Urls.Add("http://*:80");

            app.Run();
        }

        private static void SeedDatabase(PokemonsDBContext dbContext)
        {
            // Seed the database with initial data
            if (!dbContext.Pokemons.Any())
            {
                dbContext.Pokemons.AddRange(new List<Pokemon>
                {
                    new Pokemon { Name = "Pikachu", Experience = 100, Type = "Electric", PokemonID = 1 },
                    new Pokemon { Name = "Bulbasaur", Experience = 150, Type = "Grass", PokemonID = 2 },
                    new Pokemon { Name = "Charmander", Experience = 200, Type = "Fire", PokemonID = 3 }
                });
                dbContext.SaveChanges();
            }
        }
    }
}
