using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using webapi.Context;
using webapi.Repositories;
using webapi.Services;

var builder = WebApplication.CreateBuilder(args);

//com sqlite
//builder.Services.AddDbContext<ApiContext>();

//com sql server
builder.Services.AddDbContext<ApiContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<ProdutoRepository>();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<ApiContext>();
//    context.Database.EnsureCreated();
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapScalarApiReference();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
