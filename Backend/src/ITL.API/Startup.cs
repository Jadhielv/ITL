using FluentValidation;
using FluentValidation.AspNetCore;
using ITL.API.Middlewares;
using ITL.Application.Services;
using ITL.Domain.Services;
using ITL.Infrastructure.Database;
using ITL.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;
using System.Reflection;

namespace ITL.API;

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
        services.AddMvc();
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblies(Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load));

        services.AddDbContext<KCTestContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("KCTestContext")));

        services.SetupUnitOfWork();

        services.AddAutoMapper(Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load));

        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IPermissionTypeService, PermissionTypeService>();

        var client = Configuration.GetSection("ClientHost").Value;

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
               builder => builder
                .WithOrigins(Configuration.GetSection("ClientHost").Value)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
              );
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ITL.API", Version = "v1" });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ITL.API v1"));
        }

        app.UseHttpsRedirection();

        app.UseCors("CorsPolicy");

        // Use Exception Middleware
        app.UseMiddleware<ExceptionHandler>();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
