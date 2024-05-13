using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UBB_Trips.Data;
using UBB_Trips.Repository;
using UBB_Trips.Services;
using Mapster;
using FluentValidation.AspNetCore;
using UBB_Trips.Validators; 
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace UBB_Trips
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<TripContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TripContext>();
            builder.Services.AddRazorPages();

            builder.Services.AddControllersWithViews().AddFluentValidation(fv => fv
                            .RegisterValidatorsFromAssemblyContaining<ClientViewModelValidator>()
                            .RegisterValidatorsFromAssemblyContaining<BookingViewModelValidator>()
                            .RegisterValidatorsFromAssemblyContaining<TripViewModelValidator>());


            //builder.Services.AddDatabaseDeveloperPageExceptionFilter();
           // builder.Services.AddControllersWithViews();
        
            builder.Services.AddScoped<ITripRepository, TripRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();

            builder.Services.AddScoped<ITripService, TripService>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IBookingService, BookingService>();

            TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true); 


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<TripContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                 var roles = new[] { "Admin", "Booking Agent", "Client"};

                 foreach (var role in roles)
                     if (!await roleManager.RoleExistsAsync(role))
                         await roleManager.CreateAsync(new IdentityRole(role)); 

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                string email = "admin@mail.com";
                string password = "Test@1234";

                if(await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new IdentityUser() { UserName = email, Email = email, EmailConfirmed = true };

                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Admin"); 
                }

                email = "client@mail.com";
                password = "Test@1234";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new IdentityUser() { UserName = email, Email = email, EmailConfirmed = true };

                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Client");
                }

                email = "booking@mail.com";
                password = "Test@1234";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new IdentityUser() { UserName = email, Email = email, EmailConfirmed = true };

                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Booking Agent");
                }

            }
            app.Run();
        }
    }
}
