namespace Perfumes.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configura serviços
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

            app.MapPerfumesEndpoints(); // Acessa os endpoints da tabela de perfumes
            app.MapPerfumistasEndpoints(); // Acessa os endpoints da tabela de perfumistas

            app.Run();
        }
    }
}
