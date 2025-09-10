namespace Perfumes.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configura servi�os
            builder.Services
                .AddDatabase(builder.Configuration)
                .AddJsonOptions()
                .AddSwaggerDocumentation()
                .AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
                app.UseSwaggerDocumentation();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            EndpointsPerfumes.MapPerfumesEndpoints(app); // Acessa os endpoints da tabela de perfumes
            EndpointsPerfumistas.MapPerfumistasEndpoints(app); // Acessa os endpoints da tabela de perfumistas

            app.Run();
        }
    }

    /// <summary>
    /// Classe de servi�os adicionais
    /// </summary>
    public static class ServicesExtension
    {
        // M�todo respons�vel por registrar o DbContext no ciclo de vida
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration builder)
        {
            var connectionString = builder.GetConnectionString("Default");
            services.AddDbContext<Context>(options => options.UseSqlite(connectionString).LogTo(Console.WriteLine, LogLevel.Information));

            return services;
        }

        // M�todo respons�vel por fazer os ciclos infinitos n�o ocorrerem com .Include()
        public static IServiceCollection AddJsonOptions(this IServiceCollection services)
        {
            services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.SerializerOptions.WriteIndented = true;
            });

            return services;
        }

        // M�todo respons�vel por inserir o Swagger
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Perfumes",
                    Version = "v1",
                    Description = "API did�tica com perfumistas e perfumes",
                });
            });

            return services;
        }

        // M�todo respons�vel por abrir o Swagger
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Perfumes v1");
                c.RoutePrefix = string.Empty; // Abre o Swagger na raiz
            });

            return app;
        }
    }
}
