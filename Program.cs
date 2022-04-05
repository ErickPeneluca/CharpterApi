using Charpter.WebApi.Contexts;
using Charpter.WebApi.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1",new Microsoft.OpenApi.Models.OpenApiInfo {Version = "v1",Title = "CharpterWebApi"});
    }
    
    );

builder.Services.AddScoped<CharpterContext, CharpterContext>();
builder.Services.AddTransient<LivroRepository, LivroRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json","CharpterWebApi");
    c.RoutePrefix = String.Empty;
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();