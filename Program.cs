using Microsoft.EntityFrameworkCore;
using Backend_Prueba_Tecnica.Data;
using Backend_Prueba_Tecnica.Models;

var builder = WebApplication.CreateBuilder(args);

// === Spring Boot Equivalencia: Configuración de DataSource y @EnableJpaRepositories ===
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";
var url = $"http://0.0.0.0:{port}";
var target = Environment.GetEnvironmentVariable("TARGET") ?? "World";

var app = builder.Build();

// === Crear tablas en la bd si no existen (Equivalente a ddl-auto=update en Spring Boot) ===
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // EnsureCreated solo crea la DB pero no usa migraciones.
    // Migrate() es el verdadero equivalente a ddl-auto=update porque aplica los cambios iterativamente.
    dbContext.Database.Migrate();
}

app.MapGet("/", () => $"Hello {target}!");

// === Spring Boot Equivalencia: @RestController + @GetMapping ===
app.MapGet("/users", async (AppDbContext db) =>
{
    // Equivalente a: userRepository.findAll()
    return await db.Users.ToListAsync();
});

// === Spring Boot Equivalencia: @RestController + @PostMapping ===
app.MapPost("/users", async (AppDbContext db, User user) =>
{
    // Equivalente a: userRepository.save(user)
    db.Users.Add(user);
    await db.SaveChangesAsync();
    return Results.Created($"/users/{user.Id}", user);
});

app.Run(url);