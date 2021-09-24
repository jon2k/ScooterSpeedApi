using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ScooterSpeedApi.Data.Database;
using ScooterSpeedApi.Data.Repository.v1;
using ScooterSpeedApi.Domain;
using ScooterSpeedApi.Messaging.Receive.Options.v1;
using ScooterSpeedApi.Messaging.Receive.Receiver.v1;
using ScooterSpeedApi.Service.v1.Command;
using ScooterSpeedApi.Service.v1.Query;
using ScooterSpeedApi.Service.v1.Services;

namespace ScooterSpeedApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddOptions();

            var serviceClientSettingsConfig = Configuration.GetSection("RabbitMq");
            var serviceClientSettings = serviceClientSettingsConfig.Get<RabbitMqConfiguration>();
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

            bool.TryParse(Configuration["BaseServiceSettings:UseInMemoryDatabase"], out var useInMemory);

            if (!useInMemory)
            {
                services.AddDbContext<ScooterSpeedContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("OrderDatabase"));
                });
            }
            else
            {
                services.AddDbContext<ScooterSpeedContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()), ServiceLifetime.Singleton);
            }

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().AddFluentValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ScooterSpeed Api",
                    Description = "A simple API to get speed Scooters",
                    Contact = new OpenApiContact
                    {
                        Name = "Novokshonov Evgeniy",
                        Email = "jon2k@mail.ru",
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext =
                        actionContext as ActionExecutingContext;

                    if (actionContext.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });

            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(ICalcScooterSpeedService).Assembly);

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IScooterSpeedRepository, ScooterSpeedRepository>();
            

            services.AddTransient<IRequestHandler<GetLastSpeedScooterQuery, ScooterSpeed>, GetLastSpeedScooterQueryHandler>();
            services.AddTransient<IRequestHandler<GetSpeedsScooterFromPeriodQuery, List<ScooterSpeed>>, GetSpeedsScooterFromPeriodQueryHandler>();
            services.AddTransient<IRequestHandler<GetSpeedsAllScootersQuery, List<ScooterSpeed>>, GetSpeedsAllScootersQueryHandler>();
            services.AddTransient<IRequestHandler<CreateScooterSpeedCommand, ScooterSpeed>, CreateScooterSpeedCommandHandler>();
            services.AddTransient<ICalcScooterSpeedService, CalcScooterSpeedService>();
            
           // if (serviceClientSettings.Enabled)
            //{
                services.AddHostedService<ScooterCoordinateReceiver>();
            //}
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
