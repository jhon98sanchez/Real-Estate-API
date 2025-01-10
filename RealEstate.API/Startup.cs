using Microsoft.OpenApi.Models;
using RealEstate.Repository.SQLServer;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using RealEstate.API.Security.Middlewares;

namespace RealEstate.API
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureService(IServiceCollection services)
        {
            services.AddDbContext<RepositoryDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionString"],
                       sqlOptions =>
                       {
                           sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                       });
            });


            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                o.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            });



            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddSignalR();
            services.ConfigureServices();
            services.AddControllers();
            services.AddEndpointsApiExplorer();


            services.AddCors(o => o.AddPolicy("PolicyTotalCustom", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

            }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"Real Estate API in {Configuration.GetSection("ASPNETCORE_ENVIRONMENT").Value}",
                    Version = "v1",
                });
                c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Description = @"API Key used for authentication. Add 'x-api-key: {your-api-key}' in the request header.",
                    Name = "x-api-key", 
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey, 
                    Scheme = "ApiKey",
                    
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            },
                            In = ParameterLocation.Header,
                            Name = "x-api-key",
                            Type = SecuritySchemeType.ApiKey
                        },
                        new List<string>()
                    }
                });
            });


            services.AddDataProtection();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"Real Estate Api Documentation v1");
                }
                c.RoutePrefix = string.Empty;
            });

            app.UseMiddleware<ApiKeyMiddleware>();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(env.ContentRootPath, "Uploads")),
                RequestPath = "/resources"
            });

            app.UseCors("PolicyTotalCustom");


            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-XSS-Protection", "1");
                context.Response.Headers.Add("X-Frame-Options", "DENY");
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add("Access-Control-Expose-Headers", "*, Authorization");
                await next();
            });

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
