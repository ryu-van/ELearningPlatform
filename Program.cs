using AutoMapper;
using E_learning_platform.Data;
using E_learning_platform.Data.SeedData; // ?? Thêm dòng này ?? g?i RoleSeedData
using E_learning_platform.Exceptions;
using E_learning_platform.Repositories;
using E_learning_platform.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")
    ));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===== Dependency Injection =====
// Repository Layer
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();


// Service Layer
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IUserService,UserService>();

// Auto Mapper Configurations
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();



using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var canConnect = dbContext.Database.CanConnect();
    Console.WriteLine($"Database connection: {(canConnect ? "SUCCESS" : "FAILED")}");

    if (canConnect && !dbContext.Roles.Any())
    {
        dbContext.Roles.AddRange(RoleSeedData.DefaultRoles);
        dbContext.SaveChanges();
        Console.WriteLine("? Seeded default roles successfully!");
    }
    else
    {
        Console.WriteLine("?? Roles already exist, skipping seed.");
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseDeveloperExceptionPage();

app.Run();
