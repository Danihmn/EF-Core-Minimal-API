using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Perfumes.WebAPI.Contexto;

namespace Perfumes.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // String de conexão do JSON
            var connectionString = builder.Configuration.GetConnectionString("Default");

            // Registrando banco de dados
            builder.Services.AddDbContext<Context>(options => options.UseSqlite(connectionString));

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Adiciona o Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Perfumes",
                    Version = "v1",
                    Description = "API para gerenciar estoque de perfumes.",

                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Perfumes v1");
                    c.RoutePrefix = string.Empty; // Abre o Swagger na raiz
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            Endpoints.EndpointsPerfumes.MapPerfumesEndpoints(app); // Acessa os endpoints da tabela de perfumes
            Endpoints.EndpointsPerfumes.MapPerfumistasEndpoints(app); // Acessa os endpoints da tabela de perfumistas

            app.Run();
        }
    }
}
