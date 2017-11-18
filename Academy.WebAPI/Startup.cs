using Academy.Application.AutoMapper;
using Academy.Domain.Services;
using Academy.Infra.CrossCutting.IoC;
using Academy.WebAPI.Configurations;
using Academy.WebAPI.ErrorHandling;
using Academy.WebAPI.Security;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Academy.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddWebApi(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.UseCentralRoutePrefix(new RouteAttribute("academy/"));
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Academy Project",
                    Description = "Academy API",
                    Contact = new Contact { Name = "Thiago Alex", Email = "thiago.alexb@gmail.com" },
                    License = new License { Name = "GIT", Url = "https://github.com/thiagoalexb/AcademyProject" }
                });
                c.AddSecurityDefinition("Bearer",
                    new ApiKeyScheme()
                    {
                        In = "header",
                        Description = "Please insert JWT with Bearer into field",
                        Name = "Authorization",
                        Type = "apiKey"
                    });
            });

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            //AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelMappingProfile());
                cfg.AddProfile(new ViewModelToDomainMappingProfile());
            });

            // .NET Native DI Abstraction
            RegisterServices(services, config.CreateMapper());

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
                                        {
                                            var paramsValidation = bearerOptions.TokenValidationParameters;
                                            paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                                            paramsValidation.ValidAudience = tokenConfigurations.Audience;
                                            paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                                            // Valida a assinatura de um token recebido
                                            paramsValidation.ValidateIssuerSigningKey = true;

                                            // Verifica se um token recebido ainda é válido
                                            paramsValidation.ValidateLifetime = true;

                                            // Tempo de tolerância para a expiração de um token (utilizado
                                            // caso haja problemas de sincronismo de horário entre diferentes
                                            // computadores envolvidos no processo de comunicação)
                                            paramsValidation.ClockSkew = TimeSpan.Zero;
                                        });

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              IHttpContextAccessor accessor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        private static void RegisterServices(IServiceCollection services, IMapper mapper)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services, mapper);
        }
    }
}
