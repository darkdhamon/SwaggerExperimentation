using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SwaggerTestAPI2.Extensions;
using SwaggerTestAPI2.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace SwaggerTestAPI2
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
            services.AddApiVersioning(
                o =>
                {
                    o.ApiVersionReader = new HeaderApiVersionReader("api-version");
                    o.DefaultApiVersion = new ApiVersion(1, 1);
                    o.AssumeDefaultVersionWhenUnspecified = true;
                });
            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1.2", new Info
                    {
                        Title = "My API",
                        Version = "v1.1",
                        Description = "Learning Swagger",
                        Contact = new Contact
                        {
                            Name = "Bronze Brown",
                            Url = "http://bronzeharoldbrown.com"
                        }
                    });
                    c.SwaggerDoc("v1.1", new Info
                    {
                        Title = "My API",
                        Version = "v1.1",
                        Description = "Learning Swagger",
                        Contact = new Contact
                        {
                            Name = "Bronze Brown",
                            Url = "http://bronzeharoldbrown.com"
                        }
                    });
                    c.SwaggerDoc("v1.0", new Info
                    {
                        Title = "My API",
                        Version = "v1.0",
                        Description = "Learning Swagger",
                        Contact = new Contact
                        {
                            Name = "Bronze Brown",
                            Url = "http://bronzeharoldbrown.com"
                        }
                    });

                    c.DocInclusionPredicate((docName, apiDesc) =>
                    {
                        var actionApiVersionModel = apiDesc.ActionDescriptor?.GetApiVersion();
                        if (actionApiVersionModel == null) return true;

                        return actionApiVersionModel.DeclaredApiVersions.Any()
                            ? actionApiVersionModel.DeclaredApiVersions.Any(v => $"v{v.ToString()}" == docName)
                            : actionApiVersionModel.ImplementedApiVersions.Any(v => $"v{v.ToString()}" == docName);
                    });
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                    c.OperationFilter<ApiVersionOperationFilter>();
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();
            app.UseApiVersioning();
            app.UseSwagger();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/Swagger/v1.0/Swagger.json", "My API v1.0");
                c.SwaggerEndpoint("/Swagger/v1.1/Swagger.json", "My API v1.1");
                c.SwaggerEndpoint("/Swagger/v1.2/Swagger.json", "My API v1.2");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}