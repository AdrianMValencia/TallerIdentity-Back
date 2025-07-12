using TallerIdentity.Application;
using TallerIdentity.Infrastructure;
using TallerIdentity.Api.Authentication;

var builder = WebApplication.CreateBuilder(args);
var Cors = "Cors";
// Add services to the container.
builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddAuthentication(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Cors,
        builder =>
        {
            builder.WithOrigins("*");
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors(Cors);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
