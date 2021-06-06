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
using Movietheater.Application.Common;
using Movietheater.Application.FilmServices;
using Movietheater.Application.MailServices;
using Movietheater.Application.ReservationServices;
using Movietheater.Application.RoomServices;
using Movietheater.Application.ScreeningServices;
using Movietheater.Application.SeatServices;
using Movietheater.Application.Statitic;
using Movietheater.Application.UserServices;
using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

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
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IBanService, BanService>();
            services.AddTransient<IFilmService, FlimService>();
            services.AddTransient<IFilmGenreService, FilmGenreService>();
            services.AddTransient<IPeopleService, PeopleService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IReservationTypeService, ReservationTypeService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient<IRoomFormatService, RoomFormatService>();
            services.AddTransient<IScreeningService, ScreeningService>();
            services.AddTransient<ISeatService, SeatService>();
            services.AddTransient<IKindOfSeatService, KindOfSeatService>();
            services.AddTransient<IkindOfScreeningService, KindOfScreeningService>();
            services.AddTransient<ISeatRowService, SeatRowService>();
            services.AddTransient<IStorageService, FileStorageService>();
            services.AddTransient<IBanService, BanService>();
            services.AddTransient<IStatiticService, StatiticService>();
            services.AddTransient<IPositionService, PositionService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IMailService, MailService>();
            // For Identity
            services.AddIdentity<User, AppRole>(
                option =>
                {
                    option.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<MovieTheaterDBContext>()
                .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2));



            services.AddDbContext<MovieTheaterDBContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("MovieTheaterDBContext")));

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
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

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