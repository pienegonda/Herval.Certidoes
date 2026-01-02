using Herval.ValueObjects.Objects;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Herval.DownloadArquivos.Infra.CrossCutting.Ioc
{
    public static class SwaggerInjection
    {

        public static void AddSwagger(this IServiceCollection services, Assembly apiAssembly)
        {
            var apiName = apiAssembly.GetName().Name;

            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = apiName,
                        Description = apiName,
                        Contact = new OpenApiContact
                        {
                            Name = "Herval",
                            Email = string.Empty
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Use under LICX"
                        }
                    });

                    // Set the comments path for the Swagger JSON and UI.
                    var xmlFile = $"{apiAssembly.GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.IncludeXmlComments(xmlPath);

                    var bearerTokenSecurityScheme = new OpenApiSecurityScheme
                    {
                        Description = "Header de autorização JWT usando esquema Bearer. Exemplo: \"Bearer {token}\"",
                        Name = "Authorization",
                        Scheme = "oauth2",
                        In = ParameterLocation.Header
                    };

                    options.AddSecurityDefinition("Bearer", bearerTokenSecurityScheme);

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            }, new List<string>()
                        }
                    });

                    MapValueObjectTypes(options);
                }
            );
        }

        private static void MapValueObjectTypes(SwaggerGenOptions options)
        {
            options.MapType<Cnpj>(() => new OpenApiSchema { Type = "string", Format = "string" });
            options.MapType<Cep>(() => new OpenApiSchema { Type = "string", Format = "string" });
            options.MapType<Cpf>(() => new OpenApiSchema { Type = "string", Format = "string" });
            options.MapType<DataNascimento>(() => new OpenApiSchema { Type = "string", Format = "string" });
            options.MapType<Documento>(() => new OpenApiSchema { Type = "string", Format = "string" });
            options.MapType<Email>(() => new OpenApiSchema { Type = "string", Format = "string" });
            options.MapType<Latitude>(() => new OpenApiSchema { Type = "number", Format = "double" });
            options.MapType<Longitude>(() => new OpenApiSchema { Type = "number", Format = "double" });
            options.MapType<Telefone>(() => new OpenApiSchema { Type = "string", Format = "string" });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "API");
            });
        }
    }
}

