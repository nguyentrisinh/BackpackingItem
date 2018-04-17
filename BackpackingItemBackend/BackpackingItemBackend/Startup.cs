using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackpackingItemBackend.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Lib.Web.Services;
using BackpackingItemBackend.Middlewares;
using BackpackingItemBackend.Models;
using BackpackingItemBackend.DataContext;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace BackpackingItemBackend
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

            #region Database and Migration 
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            #endregion

            //#region Identity

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();
            //#endregion

            //#region Database And Migration

            //services.AddDbContext<DataContext>(
            //    options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("BackpackingItemStore.Core")));

            //#endregion


            #region Identity
            // Now Authorize is not usable
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            #endregion

            #region JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = Configuration["Jwt:Issuer"],
                       ValidAudience = Configuration["Jwt:Issuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                   };
               });

            #endregion

            #region CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                .Build());
            });
            #endregion

            services.AddMvc();

            #region SwaggerDocument
            //... rest of services configuration
            services.AddSwaggerDocumentation();
            #endregion

            #region DI

            ConfigureDependencInjection(services);

            #endregion

        }

        #region Configure DI

        public void ConfigureDependencInjection(IServiceCollection services)
        {
            services.AddScoped<IDbinitializer, Dbinitializer>();

            services.AddTransient<IThrowService, ThrowService>();
        }
        #endregion

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbinitializer dbinitializer)
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbinitializer dbinitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region User JWT Authentication
            app.UseAuthentication();
            #endregion

            #region CorsPolicy
            app.UseCors("CorsPolicy");
            #endregion

            #region Middlewares

            #region GlobalExceptionMiddleware

            app.UseGlobalExceptionMiddleware();

            #endregion

            #endregion

            #region Seed Data
            dbinitializer.Init();
            #endregion


            #region StaticFiles
            // Using Swagger static files
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "ImageResources")),
                RequestPath = "/StaticFiles"
            });

            #endregion

            //.... rest of app configuration
            app.UseSwaggerDocumentation();

            app.UseMvc();

        }
    }
}
