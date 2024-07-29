using Microsoft.EntityFrameworkCore;
using Domain.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Contexts;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.TraversePath().Load();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// IOC
builder.Services.AddScoped<AppDbContext>(_ => new AppDbContext());
builder.Services.AddScoped<INotesRepository, NotesRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

