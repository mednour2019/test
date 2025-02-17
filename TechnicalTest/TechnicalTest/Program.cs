using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Test.Core.Entities;
using Test.Core.Interfaces;
using Test.Core.Mappings;
using Test.Core.Validators;
using Test.Infrastructure.Data;
using Test.Infrastructure.GenericRepository;
using Test.Infrastructure.UserRepository;
using Test.Service.UserService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(
    (options) =>
    {
        options.AddPolicy("test", (builder) =>
        {
            builder.WithOrigins("http://localhost:4200", "http://localhost")
            .AllowAnyHeader()
            .WithMethods("GET", "POST", "PUT", "DELETE")
            .WithExposedHeaders("*");
        });
    });
// Add services to the container.

builder.Services.AddControllers();
//Add my db context
builder.Services.AddDbContext<DbContextTechnicalTest>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("myCnxStringConfig")));
//builder.Services.AddScoped<User>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<User>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();

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
app.UseCors("test");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
