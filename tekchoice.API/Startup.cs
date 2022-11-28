using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using tekchoice.Data.DataContext;
using tekchoice.Data.Models;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using System;
using tekchoice.Operations.Interfaces;
using tekchoice.Operations.Services;
using tekchoice.Core.Models;
using tekchoice.Core.Services;
using tekchoice.Core.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace tekchoice.API
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
            // enabled Cors
            services.AddCors();
            services.AddControllers();

            // connection to DB
            services.AddDbContextPool<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DB")));


            // Add interfaces and services that you use in API
            services.AddScoped<ICalculo, CalculoService>();



            // Token JWT
            /*services.Configure<AuthenticationDataModel>(Configuration.GetSection("token"));
            var token = Configuration.GetSection("token").Get<AuthenticationDataModel>();

            services.AddAuthentication(z =>
            {
                z.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                z.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(z =>
            {
                z.RequireHttpsMetadata = false;
                z.SaveToken = true;
                z.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                    ValidIssuer = token.Issuer,
                    ValidAudience = token.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });*/

            // Token with Auth0 and JWT
            //string domain = $"https://{Configuration["Auth0:Domain"]}/";
            //services
            //    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.Authority = domain;
            //        options.Audience = Configuration["Auth0:Audience"];
            //        // If the access token does not have a `sub` claim, `User.Identity.Name` will be `null`. Map it to a different claim by setting the NameClaimType below.
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            NameClaimType = ClaimTypes.NameIdentifier
            //        };
            //    });
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("read:messages", policy => policy.Requirements.Add(new HasScopeRequirement("read:messages", domain)));
            //});

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://tekchoice-am-mx-providers-dev.us.auth0.com/";
                options.Audience = "https://tekchoice/api/v2/";
            });



            // Register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            ///Se agrega Swagger para la documentación del API
            services.AddSwaggerGen(options =>
            {
                string groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"challenge API {groupName}",
                    Version = groupName,
                    Description = "Architecture    for API .."
                });

                // Include 'SecurityScheme' to use JWT Authentication
                OpenApiSecurityScheme jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Agregar Token",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {{ jwtSecurityScheme, Array.Empty<string>()}});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            ///Inclusión de Swagger
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("./swagger/v1/swagger.json", "challenge Api"); // publicado
                options.RoutePrefix = string.Empty;
                options.DocumentTitle = "challenge API";
            });

            // enable for Auth0 and JWT
            app.UseAuthentication();

            app.UseRouting();
            // enable for Auth0 and JWT
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
