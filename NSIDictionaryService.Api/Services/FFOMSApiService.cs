using NSIDictionaryService.Share.DTOs;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace NSIDictionaryService.Api.Services
{
    public static class FFOMSApiService
    {
        static readonly HttpClient _httpClient;
        static readonly JsonSerializerOptions _jsonSerializerOptions;
        
        static FFOMSApiService() //Временное решение
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://nsi.ffoms.ru/nsi-int");
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
        }

        static JsonResponseDTO GetDictionaryData(string dictionaryIdentifier)
        {
            var response = _httpClient.GetAsync($"{_httpClient.BaseAddress}/api/data?identifier={dictionaryIdentifier}").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            var DTO = JsonSerializer.Deserialize<JsonResponseDTO>(data);
            return DTO;
        }
    }
}