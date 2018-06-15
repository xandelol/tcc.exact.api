
using System.IO;
using System.Text;
using AutoMapper;
using exact.api.Business;
using exact.api.Data;
using exact.api.Repository;
using exact.api.Storage;
using lavasim.business.Business;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace exact.api
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

            AddScoped(services);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "EXACT API", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                    new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Please insert JWT with Bearer into field",
                        Name = "Authorization",
                        Type = "apiKey"
                    });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "exact.api.xml"); 
                c.IncludeXmlComments(xmlPath);     
            });
            
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;

                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ValidAudience = Configuration["SiteUrl"],
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = Configuration["SiteUrl"]
                    };
                    
                });

            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Exact API");
            });

            app.UseAuthentication();
            app.UseMvc();
        }
        
        private void AddScoped(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddDbContext<ExactContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
                        
            services.AddAutoMapper();
            
            services.AddScoped<UserRepository>();
            services.AddScoped<SettingRepository>();
            services.AddScoped<GroupRepository>();
            services.AddScoped<GroupActionRepository>();
            services.AddScoped<QuestionRepository>();

            services.AddScoped<UserBusiness>();
            services.AddScoped<SettingBusiness>();
            services.AddScoped<GroupActionBusiness>();
            services.AddScoped<QuestionBusiness>();

            services.AddSingleton<IStorageRepository>(
                new StorageRepository(
                    Configuration["Storage:AccountName"], 
                    Configuration["Storage:AccountKey"]));
        }
    }
}