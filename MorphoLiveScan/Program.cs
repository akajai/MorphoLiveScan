using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MorphoLiveScan;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

builder.Services.AddControllers();

//builder.Services.AddDbContext<MorphoLiveScanContext>(options =>
//    options.UseMySql("Server=172.17.0.2;Port=3306;Database=mydatabase;Uid=username;Pwd=userpassword;Connect Timeout=30;",
//                      new MySqlServerVersion("8.0.23")));
string connectionString = "Server=127.0.0.1;Port=3306;Database=mydatabase;Uid=root;Pwd=password;Connect Timeout=60;";
builder.Services.AddDbContext<MorphoLiveScanContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion("8.0.23"), mySqlOptions =>
        mySqlOptions.EnableRetryOnFailure(
            maxRetryCount: 1,
            maxRetryDelay: TimeSpan.FromSeconds(60),
            errorNumbersToAdd: null)
        )
    );



var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger";
});



// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
