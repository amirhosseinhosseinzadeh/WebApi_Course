using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using HotelListing.Data;
using AutoMapper;
using HotelListing.Configurations;
using HotelListing.IRepository;
using HotelListing.Repository;
using System.Reflection;
using Serilog;
using Microsoft.AspNetCore.Identity;
using HotelListing.Extensions;
using HotelListing.Services;

namespace HotelListing
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
            services.AddAuthentication();
            services.AddLogging(option => option.AddSerilog());
            services.AddControllers();
            services.AddCors(o =>
            {
                o.AddPolicy("PublicPolicy", builder =>
                     builder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader());
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelListing", Version = "1.0" });
            });
            services.AddDbContext<DataBaseContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AppConnection"));
            });
            services.AddAutoMapper(Assembly.GetAssembly(typeof(MapperInitializer)));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.ConfigureIdentity();
            services.ConfigureJwt(Configuration);
            services.AddScoped<IAuthManager , AuthManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelListing v1"));

#if (!DEBUG)
                app.UseHttpsRedirection();
#endif

            app.UseCors("PublicPolicy");
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
