using AspNetCore.Authentication.ApiKey;
using AutoMapper;
using Checkout.PaymentGateway.Api.Application;
using Checkout.PaymentGateway.Api.Infrastructure.Auth;
using Checkout.PaymentGateway.Domain;
using Checkout.PaymentGateway.Domain.Common;
using Checkout.PaymentGateway.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.IO;
using System.Reflection;

namespace Checkout.PaymentGateway.Api
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
            services
                .AddMongoClassMaps()
                .AddApiKeyAuthentication()
                .AddAutoMapper(typeof(Startup))
                .AddMediatR(typeof(Startup))
                .AddLogging()
                .Configure<PaymentDbSettings>(Configuration.GetSection("PaymentDb"))
                .AddSingleton<IPaymentContext, PaymentContext>()
                .AddScoped<IBankingService, MockBankingService>()
                .AddSwagger()
                .AddControllers();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Payment Gateway V1");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoClassMaps(this IServiceCollection sc)
        {
            BsonClassMap.RegisterClassMap<Entity>(map =>
            {
                map.AutoMap();
                map.MapIdProperty(x => x.Id).SetSerializer(new GuidSerializer(BsonType.String));
                map.SetIsRootClass(true);
            });

            BsonClassMap.RegisterClassMap<Payment>(map =>
            {
                map.AutoMap();
                map.MapProperty(p => p.Card);
                map.MapProperty(p => p.Amount);
                map.MapCreator(p => new Payment(p.Card, p.Amount));
            });

            return sc;
        }

        public static IServiceCollection AddApiKeyAuthentication(this IServiceCollection services)
        {
            services
                .AddTransient<IApiKeyRepository, InMemoryApiKeyRepository>()
                .AddAuthentication(ApiKeyDefaults.AuthenticationScheme)
                .AddApiKeyInHeader<ApiKeyProvider>(opt =>
                {
                    opt.Realm = "Checkout Payment Gateway";
                    opt.KeyName = "X-API-KEY";
                });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Payment Gateway",
                    Version = "v1"
                });

                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                c.IncludeXmlComments(xmlCommentsPath);

                c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Name = "X-API-KEY",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header
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
                            }
                        },
                        new string[] {}
                    }
                });
            });

            return services;
        }
    }
}
