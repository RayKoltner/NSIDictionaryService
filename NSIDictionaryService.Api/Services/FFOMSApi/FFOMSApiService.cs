using Microsoft.Extensions.Options;
using NSIDictionaryService.Share.DTOs;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using NSIDictionaryService.Api.Settings;
using System;

namespace NSIDictionaryService.Api.Services
{
    public class FFOMSApiService : IFFOMSApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public FFOMSApiService(IOptions<FFOMSApiSettings> options)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(options.Value.ApiAdress);
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
        }

        public DictionaryDataDTO GetDictionaryData(string identifier)
        {
            var response = _httpClient.GetAsync($"{_httpClient.BaseAddress}/api/data?identifier={identifier}").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            var DTO = JsonSerializer.Deserialize<DictionaryDataDTO>(data);
            return DTO;
        }

        public DictionaryDataDTO GetDictionaryData(string identifier, string version)
        {
            var response = _httpClient.GetAsync($"{_httpClient.BaseAddress}/api/data?identifier={identifier}&version={version}").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            var DTO = JsonSerializer.Deserialize<DictionaryDataDTO>(data);
            return DTO;
        }

        public FedPackDictionariesDTO GetFedPackDictVersions()
        {
            //var response = _httpClient.GetAsync($"{_httpClient.BaseAddress}/api/fedPackDictionaryVerions").Result;
            //string data = response.Content.ReadAsStringAsync().Result;
            //var DTO = JsonSerializer.Deserialize<FedPackDictionariesDTO>(data);
            //return DTO;
            throw new NotImplementedException();
        }

        public DictionaryVersionDTO GetVersionData(string identifier)
        {
            var response = _httpClient.GetAsync($"{_httpClient.BaseAddress}/api/versions?identifier={identifier}").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            var DTO = JsonSerializer.Deserialize<DictionaryVersionDTO>(data);
            return DTO;
        }
    }
}