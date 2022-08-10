using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieTheater.Api;
using MovieTheater.Models.User;

namespace MovieTheater.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddTransient<UserApiClient, UserApiClient>();
            services.AddTransient<RoleApiClient, RoleApiClient>();
            services.AddTransient<MovieApiClient, MovieApiClient>();
            services.AddTransient<PeopleApiClient, PeopleApiClient>();
            services.AddTransient<ReservationApiClient, ReservationApiClient>();
            services.AddTransient<SeatApiClient, SeatApiClient>();
            services.AddTransient<SeatRowApiClient, SeatRowApiClient>();
            services.AddTransient<AuditoriumApiClient, AuditoriumApiClient>();
            services.AddTransient<ScreeningApiClient, ScreeningApiClient>();
            services.AddTransient<MovieCensorshipApiClient, MovieCensorshipApiClient>();
            services.AddTransient<StatisticApiClient, StatisticApiClient>();
            services.AddTransient<PositionApiClient, PositionApiClient>();
            services.AddTransient<TicketApiClient, TicketApiClient>();
            services.AddTransient<InvoiceApiClient, InvoiceApiClient>();

            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserCreateValidator>());

            services.AddSession();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Index";
                    options.AccessDeniedPath = "/User/Forbident";
                    options.LogoutPath = "/User/Logout";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}