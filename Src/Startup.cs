using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Src.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Src.Repositories;
using Src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Src
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //Create Roles
        private static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            _ = serviceProvider.GetRequiredService<UserManager<Author>>();

            string[] rolesNames = {"Admin", "Author"};

            IdentityResult result;
            foreach (var role in rolesNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);
                if(!roleExists)
                {
                    result = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Src", Version = "v1" });
            });

            //Database
            services.AddDbContextPool<FictionDbContext>(options =>
            options
            .UseMySql(
                Configuration.GetConnectionString("MariaDbConnectionString"),
                new MariaDbServerVersion(new Version(10,6,3))
            ));

            //Identity
            services.AddIdentity<Author, IdentityRole>()
                .AddEntityFrameworkStores<FictionDbContext>()
                .AddDefaultTokenProviders();

            //Auth
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            //Jwt Beare
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

            //Mapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //Repositories
            services.AddScoped<IGenericRepository<Genre>, GenericRepository<Genre>>();
            services.AddScoped<AuthorRepository>();
            services.AddScoped<HistoryRepository>();
            services.AddScoped<ChapterRepository>();
            services.AddScoped<CommentRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Src v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            CreateRoles(serviceProvider).Wait();
        }
    }
}
