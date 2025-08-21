using RCAWebApplication.Models;
using RCAWebApplication.Repository;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();
builder.Services.AddScoped<IRcaAcRepository, RCAWebApplication.Repository.RcaAcRepository>();
var app = builder.Build();

// Get connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=RcaAc}/{action=Index}/{id?}");

app.Run();

public interface IDbConnectionFactory { SqlConnection Create(); }
public class SqlConnectionFactory : IDbConnectionFactory
{
    private readonly IConfiguration _cfg;
    public SqlConnectionFactory(IConfiguration cfg) => _cfg = cfg;
    public SqlConnection Create() => new(_cfg.GetConnectionString("DefaultConnection"));
}
