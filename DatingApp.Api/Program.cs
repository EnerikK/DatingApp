using DatingApp.Api.Extensions;
using DatingApp.Api.Options;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddCors();
builder.RegisterServices(typeof(Program));


var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:4200","https://localhost:4200"));

app.RegisterPipelineComponents(typeof(Program));

app.Run();