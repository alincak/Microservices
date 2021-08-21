using FreeCource.API.Basket.Consumers;
using FreeCource.API.Basket.Services;
using FreeCource.API.Basket.Settings;
using FreeCourse.Shared.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;

namespace FreeCource.API.Basket
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
      JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

      services.AddControllers(options => 
      {
        var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        options.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
      });

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
      {
        options.Authority = Configuration["IdentityServerUrl"];
        options.Audience = "resource_basket";
        options.RequireHttpsMetadata = false;
      });

      services.AddHttpContextAccessor();

      services.AddMassTransit(x =>
      {
        x.AddConsumer<CourseNameChangedEventConsumer>();

        //default port: 5672
        x.UsingRabbitMq((context, cfg) =>
        {
          cfg.Host(Configuration["RabbitMQUrl"], "/", host =>
          {
            host.Username("guest");
            host.Password("guest");
          });

          cfg.ReceiveEndpoint("course-name-changed-event-basket-service", e =>
          {
            e.ConfigureConsumer<CourseNameChangedEventConsumer>(context);
          });
        });
      });

      services.AddMassTransitHostedService();

      services.AddScoped<ISharedIdentityService, SharedIdentityService>();
      services.AddScoped<IBasketService, BasketService>();

      services.Configure<RedisSettings>(Configuration.GetSection("RedisSettings"));
      services.AddSingleton<IRedisSettings>(sp =>
      {
        return sp.GetRequiredService<IOptions<RedisSettings>>().Value;
      });

      services.AddSingleton<IRedisService>(sp =>
      {
        var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
        var redisService = new RedisService(redisSettings.Host, redisSettings.Port);

        return redisService;
      });

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "FreeCource.API.Basket", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FreeCource.API.Basket v1"));
      }

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
