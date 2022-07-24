using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MovieTheater.Application.Common;
using MovieTheater.Application.MailServices;
using MovieTheater.Application.RoomServices;
using MovieTheater.Application.ScreeningServices;
using MovieTheater.Application.Statitic;
using MovieTheater.Application.UserServices;
using MovieTheater.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using MovieTheater.Application.CustomerServices;
using MovieTheater.Application.FilmServices.MovieCensorshipes;
using MovieTheater.Data.Models;
using MovieTheater.Application.FilmServices.MovieGenres;
using MovieTheater.Application.FilmServices.Movies;
using MovieTheater.Application.FilmServices.Actors;
using MovieTheater.Application.FilmServices.Positions;
using MovieTheater.Application.ReservationServices.Reservations;
using MovieTheater.Application.ReservationServices.ReservationTypes;
using MovieTheater.Application.ReservationServices.Tickets;
using MovieTheater.Application.RoomServices.Auditoriums;
using MovieTheater.Application.RoomServices.RoomFormats;
using MovieTheater.Application.ScreeningServices.Screenings;
using MovieTheater.Application.SeatServices.Seats;
using MovieTheater.Application.SeatServices.SeatRows;
using MovieTheater.Application.SeatServices.SeatTypes;
using MovieTheater.Application.TimeServices;
using MovieTheater.BackEnd.Hub;

namespace MovieTheater.BackEnd
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
            services.AddHttpContextAccessor();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ITimeService, TimeService>();
            services.AddTransient<IMovieCensorshipService, MovieCensorshipService>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IMovieGenreService, MovieGenreService>();
            services.AddTransient<IActorService, ActorService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IReservationTypeService, ReservationTypeService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuditoriumService, AuditoriumService>();
            services.AddTransient<IAuditoriumFormatService, AuditoriumFormatService>();
            services.AddTransient<IScreeningService, ScreeningService>();
            services.AddTransient<ISeatService, SeatService>();
            services.AddTransient<ISeatTypeService, SeatTypeService>();
            services.AddTransient<ISeatRowService, SeatRowService>();
            services.AddTransient<IStorageService, FileStorageService>();
            services.AddTransient<IMovieCensorshipService, MovieCensorshipService>();
            services.AddTransient<IStatisticService, StatisticService>();
            services.AddTransient<IPositionService, PositionService>();
            services.AddTransient<IMailService, MailService>();


            // For Identity


            services.AddDbContext<MoviesContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MovieDBContext")));

            // Add JWT Authenticate
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })

                // Adding Jwt Bearer
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = Configuration["JWT:ValidAudience"],
                        ValidIssuer = Configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger MoiveTheater", Version = "V1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
            var mailsettings = Configuration.GetSection("MailSettings");
            services.Configure<MailSettings>(mailsettings);
            services.AddControllersWithViews();
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            }); ;
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ReservationHub>("/reservationHub");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}