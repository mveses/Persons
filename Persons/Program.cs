using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persons.Data;
using Persons.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PersonsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PersonsContext") ?? throw new InvalidOperationException("Connection string 'PersonsContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddKendo();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
