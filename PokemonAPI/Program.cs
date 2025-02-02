using PokemonAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var pokemons = new List<Pokemon>
{
    new Pokemon { Id = 1, Name = "Bulbasaur", Type = "Grass" },
    new Pokemon { Id = 2, Name = "Ivysaur", Type = "Grass" },
    new Pokemon { Id = 2, Name = "Venosaur", Type = "Grass" },
    new Pokemon { Id = 3, Name = "Charmander", Type = "Fire" }
};

//Create
app.MapPost("/pokemon", (Pokemon pokemon) =>
{
    pokemons.Add(pokemon);
    return Results.Ok(pokemon);
});

// Read all
app.MapGet("/pokemons", () =>
{
    return Results.Ok(pokemons);
});


//Read by ID
app.MapGet("/pokemon/{id}", (int id) =>
{
    var pokemon = pokemons.Find(p => p.Id == id);

    if (pokemon == null)
    {
        return Results.NotFound("Sorry,this pokemon does not exist");
    }

    return Results.Ok(pokemon);
});

//update
app.MapPut("/pokemon/{id}", (Pokemon updatePokemon, int id) =>
{
    var pokemon = pokemons.Find(p => p.Id == id);
    if (pokemon == null)
    {
        return Results.NotFound("This pokemon does not exist mate");
    }

  
    pokemons[id] = updatePokemon;
    
    
    return Results.Ok(pokemon);
});

//Delete
app.MapDelete("/pokemon/{id}", (int id) =>
{
    var pokemon = pokemons.Find(p => p.Id == id);

    if (pokemon == null)
    {
        return Results.NotFound("not found");
    }

    pokemons.Remove(pokemon);

    return Results.Ok("works");
});


app.Run();
