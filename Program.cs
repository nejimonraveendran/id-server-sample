using Duende.IdentityServer;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using id_server.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer; // Add this line

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Server=.;Database=IdentityServerDb;User ID=sa;Password=Admin@123;TrustServerCertificate=True;";

builder.Services.AddDbContext<ConfigurationDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("id-server")));
builder.Services.AddDbContext<PersistedGrantDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("id-server")));

builder.Services.AddIdentityServer()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(connectionString, x => x.MigrationsAssembly("id-server"));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(connectionString, x => x.MigrationsAssembly("id-server"));
    });

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "http://localhost:5221"; // Update to your IdentityServer URL/port if different
        options.RequireHttpsMetadata = false; // Set to true in production
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = false,
            
        };
    });

builder.Services.AddControllers();

var app = builder.Build();

// Seed IdentityServer database with a sample client
IdentityServerSeeder.SeedClients(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseIdentityServer();
app.UseAuthentication(); // Add this line
app.UseAuthorization();
app.MapControllers();

app.Run();
