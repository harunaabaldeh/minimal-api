using Application.Abstractions;
using Application.Posts.Commands;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<SocialDbContext>(opt => opt.UseSqlite(cs));

builder.Services.AddScoped<IPostRepository, PostRepositories>();

var app = builder.Build();


app.UseHttpsRedirection();


app.Run();


