using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using DnDBeyond.DB;
using DnDBeyond.Models;
using DnDBeyond.Services;
using DnDBeyond.Services.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DnDBeyond
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
            services.AddDbContext<CharactersContext>(opt => opt.UseInMemoryDatabase("Characters"));
            services.AddScoped<ICharactersService, CharactersService>();
            services.AddScoped<IHitPointsService, HitPointsService>();
            services.AddScoped<IDamageService, DamageService>();
            services.AddScoped<IHealService, HealService>();
            services.AddScoped<IDiceService, DiceService>();
            services.AddScoped<CharactersRepository>();
            services.AddControllers();
            services.AddMvc()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "DDB API",
                    Description = "A solution to the <a href=\"https://github.com/DnDBeyond/ddb-back-end-developer-challenge\">DDB Back End Developer Challenge</a>",
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "It's DnD (DnD Beyond)");
            });
        }
    }
}
