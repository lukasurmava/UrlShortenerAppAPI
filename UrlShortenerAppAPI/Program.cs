using Microsoft.EntityFrameworkCore;
using System;
using UrlShortenerApp.Infrastructure.Abstractions;
using UrlShortenerApp.Infrastructure.Data;
using UrlShortenerApp.Infrastructure.Concrete;
using UrlShortenerApp.Service.Abstractions;
using UrlShortenerApp.Service.Concrete;
using UrlShortenerApp.API.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<UrlShortenerAppDbContext>(options =>
    options.UseInMemoryDatabase("UrlShortenerDb"));
builder.Services.AddScoped<IOriginalUrlRepository, OriginalUrlRepository>();
builder.Services.AddScoped<IAnalyticRepository, AnalyticRepository>();
builder.Services.AddScoped<IOriginalUrlService, OriginalUrlService>();
builder.Services.AddScoped<IAnalyticService, AnalyticService>();
builder.Services.AddHostedService<ExpiredUrlCleanupBackgroundService>();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
