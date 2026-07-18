using E_Commerce.API.Middleware;
using E_Commerce.Application;
using E_Commerce.Infrastructure;
using E_Commerce.Infrastructure.Identity;
using E_Commerce.Infrastructure.Identity.Seeding;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

// to register our application services (MediatR handlers)
builder.Services.AddApplicationServices();

// we registered our infrastructure services (DbContext)

builder.Services.AddInfrastructureServices(builder.Configuration);

//// swagger

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();


// Seed identity into the database after startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

    await IdentitySeeder.SeedAsync(userManager, roleManager);
}

// enable swagger
app.UseSwagger();

app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
