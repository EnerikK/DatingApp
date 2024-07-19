using DatingApp.Api.Extensions;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.RegisterServices(typeof(Program));

var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:4200","https://localhost:4200"));

app.RegisterPipelineComponents(typeof(Program));

app.Run();