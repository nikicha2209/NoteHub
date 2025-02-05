using Microsoft.EntityFrameworkCore;
using NoteHub.Data;
using NoteHub.Services.Data;
using NoteHub.Services.Data.Interfaces;

namespace NoteHub.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Регистрация на DbContext
            builder.Services.AddDbContext<NotesDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Регистрация на NoteService
            builder.Services.AddScoped<INoteService, NoteService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Note}/{action=Index}/{id?}");

            app.Run();
        }
    }
}