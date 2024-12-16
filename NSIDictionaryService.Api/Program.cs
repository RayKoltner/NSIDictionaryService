
using Microsoft.EntityFrameworkCore;
using NSIDictionaryService.Api.Extensions;
using NSIDictionaryService.Api.Settings;
using NSIDictionaryService.Data;

namespace NSIDictionaryService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<FFOMSApiSettings>(
                builder.Configuration.GetSection(FFOMSApiSettings.position));

            // Database
            string TFOMSContextConnection = builder.Configuration.GetConnectionString("DbConnection") ??
                throw new InvalidOperationException("");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(TFOMSContextConnection));

            // Add services to the container.
            builder.Services.AddControllers();
            //builder.AddUploadServices();
            builder.AddOtherServices(); //Currently in development

            builder.AddRepositories();

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
