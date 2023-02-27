using DapperSampleProject.Helpers;
using DapperSampleProject.IRepository;
using DapperSampleProject.Models;
using DapperSampleProject.Repositories;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ConnectionHelper>();

builder.Services.AddScoped<DoctorRepository>();

builder.Services.AddScoped<PatientRepository>();



builder.Services.AddScoped<IGenericRepository<Doctor>, DoctorRepository>();

builder.Services.AddScoped<IGenericRepository<Patient>, PatientRepository>();


var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
