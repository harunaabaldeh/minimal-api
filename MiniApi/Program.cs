using Application.Abstractions;
using Application.Posts.Commands;
using Application.Posts.Queries;
using Application.Posts.QueryHandlers;
using DataAccess;
using DataAccess.Repositories;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<SocialDbContext>(opt => opt.UseSqlite(cs));

builder.Services.AddScoped<IPostRepository, PostRepository>();

var app = builder.Build();


app.UseHttpsRedirection();

app.MapGet("/api/posts/{id}", async (IMediator mediator, int id) =>
{
    var getPost = new GetPostById { PostId = id };
    var post = await mediator.Send(getPost);
    return Results.Ok();
}).WithName("GetPostById");

app.MapPost("/api/posts", async ([FromServices] IMediator mediator, [FromBody] Post post) =>
{
    var createPost = new CreatePost { PostContent = post.Content };
    var createdPost = await mediator.Send(createPost);
    return Results.CreatedAtRoute("GetById", new { createdPost.Id }, createdPost);
});

app.MapGet("/api/posts", (IMediator mediator) =>
{
    var getCommand = new GetAllPosts();
    var posts = mediator.Send(getCommand);
    return Task.FromResult(Results.Ok(posts));
});

app.MapPut("/api/posts/{id}", async (IMediator mediator, Post post, int id) =>
{
    var updatePost = new UpdatePost { PostId = id, UpdateContent = post.Content };
    var updatedPost = await mediator.Send(updatePost);
    return Results.Ok(updatedPost);
});

app.MapDelete("/api/posts/{id}", (IMediator mediator, int id) =>
{
    var deletePost = new DeletePost { PostId = id };
    var deletedPost = mediator.Send(deletePost);
    return Task.FromResult(Results.NoContent());
});

app.Run();


