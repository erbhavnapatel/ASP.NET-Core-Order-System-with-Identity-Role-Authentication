using Order_System.Entities;
using Order_System.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_System
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<OrderDatabaseContext>(op => op.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<OrderDatabaseContext>()
                .AddDefaultTokenProviders();

            services.AddSwaggerGen(c =>
            {
                ////This is to generate the Default UI of Swagger Documentation  
                //c.SwaggerDoc("v1", new OpenApiInfo
                //{
                //    Version = "v1",
                //    Title = "Order-System-Application",
                //    Description = "ASP.NET Core 3.1 Web API"
                //});

                //// To Enable authorization using Swagger (JWT)  
                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                //{
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer",
                //    BearerFormat = "JWT",
                //    In = ParameterLocation.Header,
                //    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                //});

                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //          new OpenApiSecurityScheme
                //            {
                //                Reference = new OpenApiReference
                //                {
                //                    Type = ReferenceType.SecurityScheme,
                //                    Id = "Bearer"
                //                }
                //            },
                //            new string[] {}

                //    }
                //});

            });

        //    services.AddTransient<IOrderRepository, OrderRepository>();
        //    services.AddTransient<IProductRepository, ProductRepository>();

        //    //Inject AppSettings
        //    services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

        //    services.Configure<IdentityOptions>(options =>
        //    {
        //        options.Password.RequireDigit = false;
        //        options.Password.RequireNonAlphanumeric = false;
        //        options.Password.RequireLowercase = false;
        //        options.Password.RequireUppercase = false;
        //        options.Password.RequiredLength = 7;
        //    });

        //    //Jwt Authentication

        //    var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());

        //    services.AddAuthentication(x =>
        //    {
        //        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //        .AddJwtBearer(x =>
        //        {
        //            x.RequireHttpsMetadata = false;
        //            x.SaveToken = false;
        //            x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        //            {
        //                ValidateIssuerSigningKey = true,
        //                IssuerSigningKey = new SymmetricSecurityKey(key),
        //                ValidateIssuer = false,
        //                ValidateAudience = false,
        //                ClockSkew = TimeSpan.Zero
        //            };
        //        });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            //app.Use(async (ctx, next) =>
            //{
            //    await next();
            //    if (ctx.Response.StatusCode == 204)
            //    {
            //        ctx.Response.ContentLength = 0;
            //    }
            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DefaultModelsExpandDepth(-1);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DefaultConnection");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
