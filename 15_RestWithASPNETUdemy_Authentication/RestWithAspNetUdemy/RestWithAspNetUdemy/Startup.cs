using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using RestWithAspNetUdemy.Business;
using RestWithAspNetUdemy.Business.Implementations;
using RestWithAspNetUdemy.Configurations;
using RestWithAspNetUdemy.Model.Context;
using RestWithAspNetUdemy.Repository;
using RestWithAspNetUdemy.Repository.Generic;
using RestWithAspNetUdemy.Services;
using RestWithAspNetUdemy.Services.Implementations;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestWithAspNetUdemy
{
    public class Startup
    {
        private const string _versaoAPI = "v1";
        private const string _nomeAPI = "REST API's from 0 to Azure with ASP.NET Core 5 and Docker";
        private const string _apiDesc = "API RESTful developed  in course";

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }
        
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Token Configuration.
            var tokenConfigurations = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                    Configuration.GetSection("TokenConfigurations") // item AppSettings.json
            ).Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenConfigurations.Issuer,
                    ValidAudience = tokenConfigurations.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            // Configurando CORS.
            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            services.AddControllers();

            var connection = Configuration["MySQLConnection:MySQLConnectionString"];

            if (Environment.IsDevelopment())
            {
                MigrationDataBase(connection);
            }

            // Adiciona formato XML.
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            })
            .AddXmlSerializerFormatters();

            services.AddDbContext<MySqlContext>(options => options.UseMySql(connection));

            // Versionamento API.
            services.AddApiVersioning();

            // Adicionado recurso Swagger.
            services.AddSwaggerGen(c => {
                c.SwaggerDoc(
                    _versaoAPI,
                    new OpenApiInfo
                    {
                        Title = _nomeAPI,
                        Version = _versaoAPI,
                        Description = $"{_apiDesc} '{_nomeAPI}'",
                        Contact = new OpenApiContact
                        {
                            Name = "Thiago Dias",
                            Url = new Uri("https://github.com/thsdias")
                        }
                    });
            });

            // Injecao de Dependencia.
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            services.AddScoped<IBooksBusiness, BooksBusinessImplementation>();
            services.AddScoped<ILoginBusiness, LoginBusinessImplementation>();

            services.AddTransient<ITokenService, TokenService>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Habilita CORS na aplicacao.
            app.UseCors();

            #region swagger

            // Responsavel por gerar json com documentacao.
            app.UseSwagger();

            // Responsavel por gerar uma pagina html para visualizacao da documentacao.
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint($"/swagger/{_versaoAPI}/swagger.json",
                    $"{_nomeAPI} - {_versaoAPI}");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            #endregion

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void MigrationDataBase(string connection)
        {
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true,
                };

                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw;
            }
        }
    }
}
