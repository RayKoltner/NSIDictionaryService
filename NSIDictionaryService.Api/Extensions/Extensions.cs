using NSIDictionaryService.Api.Repositories;
using NSIDictionaryService.Api.Repositories.Dictionaries;
using NSIDictionaryService.Api.Repositories.Upload;
using NSIDictionaryService.Api.Services;
using NSIDictionaryService.Api.Services.Handlers;
using NSIDictionaryService.Api.Services.UploadDictionary;

namespace NSIDictionaryService.Api.Extensions
{
    public static class Extensions
    {
        public static IHostApplicationBuilder AddRepositories(this IHostApplicationBuilder builder)
        {
            builder.Services.AddTransient<IDictPropertyRepository, DictPropertyRepository>();
            builder.Services.AddTransient<IDictVersionRepository, DictVersionRepository>();
            builder.Services.AddTransient<IUploadInfoRepository, UploadInfoRepository>();
            builder.Services.AddTransient<IChangeRepository, ChangeRepository>();
            builder.Services.AddTransient<IV006Repository, V006Repository>();

            return builder;
        }

        public static IHostApplicationBuilder AddUploadServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<V006Uploader>();

            return builder;
        }

        public static IHostApplicationBuilder AddOtherServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IFFOMSApiService, FFOMSApiService>();

            return builder;
        }
    }
}
