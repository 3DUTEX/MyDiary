using Domain.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Contexts;
using Domain.Services.Users;
using Application.Services;
using Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Loading DOTENV
DotNetEnv.Env.TraversePath().Load();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddJWTAuth();

// IOC
builder.Services.AddScoped<AppDbContext>(_ => new AppDbContext());
builder.Services.AddScoped<INotesRepository, NotesRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Authorization
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

