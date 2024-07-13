using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Backend_developer_Test.Models;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Backend_developer_Test.Services;

namespace Backend_developer_Test
{
    internal class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            Backend_developer_Test.Models.Configuration.JWTConfig jwtConfig = Configuration.GetSection("jwtConfig").Get<Backend_developer_Test.Models.Configuration.JWTConfig>();
            services.AddDbContext<Backend_developer_Test.Models.AppDBContext>((p, o) =>
            o.UseSqlServer(Configuration.GetConnectionString("AppDB")/*, new MySQlServerVersion(new Version(8, 0, 28)*/)
            );
            services.AddControllers();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidAudience = jwtConfig.Issuer,
                    ValidIssuer = jwtConfig.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key))
                };
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "JWTToken_Auth_API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    }
                },
                new string[] {}
            }
             });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
            });


            #region Service Registration

                   services.AddSingleton(jwtConfig); //Services are created the first time they are requested and then shared across all requests.
                   //services.AddAutoMapper(typeof(Startup));
                   services.AddControllersWithViews();
            //     services.AddTransient(typeof(ILoginService), typeof(LoginService)); //Services are created each time they are requested.
            services.AddTransient(typeof(IDepartmentService), typeof(DepartmentService)); //Services are created each time they are requested.

            // Transient: new instance per request

            #endregion


            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "AllowOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                    });
            });
            //    var mappingConfig = new MapperConfiguration(mc =>
            //    {
            //        mc.AddProfile(new MappingProfiles());
            //    });

            //    IMapper mapper = mappingConfig.CreateMapper();
            //}
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "NEIR_API v1"));
            }
            app.UseAuthentication();

            app.UseRouting();
            app.UseCors("AllowOrigin");
            app.UseHttpsRedirection();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Test API V1");
            });
            app.UseDeveloperExceptionPage();
            //app.UseMiddleware(typeof(NEIR_Middleware));
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseDeveloperExceptionPage();
        }
    }
}
