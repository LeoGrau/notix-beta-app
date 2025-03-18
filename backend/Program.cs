using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Notix.Beta.API.Mapping;
using Notix.Beta.API.Notes.Domain.Models;
using Notix.Beta.API.Notes.Domain.Repositories;
using Notix.Beta.API.Notes.Domain.Services;
using Notix.Beta.API.Notes.Repositories;
using Notix.Beta.API.Notes.Services;
using Notix.Beta.API.Shared.Domain.Repositories;
using Notix.Beta.API.Shared.Persistence.Context;
using Notix.Beta.API.Shared.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Notix Beta API",
            Description = "Notix Beta API",
        });
        options.EnableAnnotations();
    });

var connectionString = builder.Configuration.GetConnectionString("NotixDBConnection");

// Add DB Context
builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    if (!string.IsNullOrEmpty(connectionString))
    {
        optionsBuilder
            .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)))
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging(false)
            .EnableDetailedErrors();
    }
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add Controllers
builder.Services.AddControllers();

// CORS
builder.Services.AddCors();


// Services and Scoped
builder.Services.AddScoped<IBaseRepository<Note, int>, NoteRepository>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<INoteService, NoteService>();

builder.Services.AddScoped<IBaseRepository<Category, int>, CategoryRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddAutoMapper(
    typeof(ModelToResourceProfile),
    typeof(ResourceToModelProfile));

var app = builder.Build();

// Validating database creation
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetRequiredService<AppDbContext>())
{
    // context.Database.Migrate();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
// Allow Swagger for testing purposes.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("v1/swagger.json", "v1");
    options.RoutePrefix = "swagger";
});

app.UseCors(policyBuilder => policyBuilder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapControllers();

app.UseHttpsRedirection();

app.Run();