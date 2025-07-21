using Br.Gov.Sp.Fazenda.SisCoi.IoC;
using Br.Gov.Sp.Fazenda.SisCoi.WebApi.WsFederation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Br.Gov.Sp.Fazenda.SisCoi.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.WithOrigins(_configuration["Cliente:allowedCorsOrigins"]).AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Secreta"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = WsFederationAuthenticationDefaults.AuthenticationType;
                options.DefaultChallengeScheme = WsFederationAuthenticationDefaults.AuthenticationType;
            })
            .AddCustomAuthentication(options =>
            {
                options.Wtrealm = _configuration["SefazIdentity:Wtrealm"];
                options.MetadataAddress = _configuration["SefazIdentity:MetadataAddress"];
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Br.Gov.Sp.Fazenda.SisCoi.WebApi",
                    Version = "v1",
                    Description = "SisCOI API - Backend",
                    Contact = new OpenApiContact { Name = "Rafael Wellington Cerqueira de Jesus", Email = "rwcjesus@prodesp.fazenda.sp.gov.br" },
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header usa o Bearer esquema. " +
                    "\r\n\r\n Digite 'Bearer' [espaço] e adicione seu token." +
                    "\r\n\r\n Exemplo: Bearer 123456abcdef"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            services.AddControllersWithViews();
            services.AddMediatR(typeof(Startup));
            RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
        }
        private static void RegisterServices(IServiceCollection services)
        {
            NativeInjectBootStrapper.RegisterServices(services);
        }

    }
}
