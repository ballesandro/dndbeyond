using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using DnDBeyond.DB;
using DnDBeyond.GraphQL_;
using DnDBeyond.GraphQL_.Inputs;
using DnDBeyond.GraphQL_.Types;
using DnDBeyond.Models;
using DnDBeyond.Services;
using DnDBeyond.Services.Implementations;
using GraphiQl;
using GraphQL;
using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
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
            services.AddSingleton<ICharactersService, CharactersService>();
            services.AddSingleton<IHitPointsService, HitPointsService>();
            services.AddSingleton<IDamageService, DamageService>();
            services.AddSingleton<IHealService, HealService>();
            services.AddSingleton<IDiceService, DiceService>();
            services.AddSingleton<CharactersRepository>();

            services.AddControllers();
            services.AddMvc()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            // Swagger
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

            // GraphQL
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddSingleton<DnDBeyondSchema>();
            services.AddSingleton<DnDBeyondQuery>();
            services.AddSingleton<DnDBeyondMutation>();
            services.AddSingleton<CharacterClassType>();
            services.AddSingleton<CharacterDefenseType>();
            services.AddSingleton<CharacterStatsType>();
            services.AddSingleton<CharacterType>();
            services.AddSingleton<DefenseDegreeType>();
            services.AddSingleton<HitPointsMethodType>();
            services.AddSingleton<ItemType>();
            services.AddSingleton<ModifierType>();
            services.AddSingleton<CharacterClassInput>();
            services.AddSingleton<CharacterDefenseInput>();
            services.AddSingleton<CharacterInput>();
            services.AddSingleton<CharacterStatsInput>();
            services.AddSingleton<ItemInput>();
            services.AddSingleton<ModifierInput>();

            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
                options.ExposeExceptions = true;
            });
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
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

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "It's DnD (DnD Beyond)");
            });

            // GraphQL
            app.UseGraphQL<DnDBeyondSchema>("/graphql");
            app.UseGraphiQl("/graphiql");
        }
    }
}
