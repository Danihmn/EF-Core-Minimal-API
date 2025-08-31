using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Perfumes.WebAPI.Contexto;
using Perfumes.WebAPI.Endpoints;

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

            // Configuração para evitar ciclos infinitos nas consultas
            builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.SerializerOptions.WriteIndented = true;
            });

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
                    Description = "API voltada para fins didáticos, utiliza exemplo de relacionamento entre perfumistas e seus perfumes",
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

            EndpointsPerfumes.MapPerfumesEndpoints(app); // Acessa os endpoints da tabela de perfumes
            EndpointsPerfumistas.MapPerfumistasEndpoints(app); // Acessa os endpoints da tabela de perfumistas

            app.Run();
        }
    }
}
