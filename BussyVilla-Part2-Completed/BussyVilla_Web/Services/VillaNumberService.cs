using BussyVilla_Utility;
using BussyVilla_Web.Models.Dto;
using BussyVilla_Web.Services.IServices;
using Newtonsoft.Json.Linq;

namespace BussyVilla_Web.Services
{
    public class VillaNumberService : IVillaNumberService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl; 
        private readonly IBaseService _baseService;

        public VillaNumberService(IHttpClientFactory clientFactory, IConfiguration configuration, IBaseService baseService)
        {
            _baseService = baseService;
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public async Task<T> CreateAsync<T>(VillaNumberCreateDTO dto)
        {
            return await _baseService.SendAsync<T>(new Models.APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = villaUrl + $"/api/{SD.CurrentAPIVersion}/villaNumberAPI"
            });
        }

        public async Task<T> DeleteAsync<T>(int id)
        {
            return await _baseService.SendAsync<T>(new Models.APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = villaUrl + $"/api/{SD.CurrentAPIVersion}/villaNumberAPI/"+id
            });
        }

        public async Task<T> GetAllAsync<T>()
        {
            return await _baseService.SendAsync<T>(new Models.APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + $"/api/{SD.CurrentAPIVersion}/villaNumberAPI",
            });
        }


        public async Task<T> GetAsync<T>(int id)
        {
            return await _baseService.SendAsync<T>(new Models.APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + $"/api/{SD.CurrentAPIVersion}/villaNumberAPI/" + id
            });
        }

        public async Task<T> UpdateAsync<T>(VillaNumberUpdateDTO dto)
        {
            return await _baseService.SendAsync<T>(new Models.APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + $"/api/{SD.CurrentAPIVersion}/villaNumberAPI/" + dto.VillaNo,
            });
        }
    }
}
