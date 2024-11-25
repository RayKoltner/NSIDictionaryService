
using NSIDictionaryService.Api.Services;
using NSIDictionaryService.Api.Settings;

namespace NSIDictionaryService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<FFOMSApiSettings>(
                builder.Configuration.GetSection(FFOMSApiSettings.position));

            // Add services to the container.
            builder.Services.AddSingleton<IFFOMSApiService, FFOMSApiService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
