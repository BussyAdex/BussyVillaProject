﻿using BussyVilla_Utility;
using BussyVilla_Web.Models.Dto;
using BussyVilla_Web.Services.IServices;

namespace BussyVilla_Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl;
        private readonly IBaseService _baseService;

        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration, IBaseService baseService)
        {
            _baseService = baseService;
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public async Task<T> LoginAsync<T>(LoginRequestDTO obj)
        {
            return await _baseService.SendAsync<T>(new Models.APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = villaUrl + $"/api/{SD.CurrentAPIVersion}/UsersAuth/login"
            }, 
            withBearer:false
            );
        }

        public async Task<T> RegisterAsync<T>(RegisterationRequestDTO obj)
        {
            return await _baseService.SendAsync<T>(new Models.APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = villaUrl + $"/api/{SD.CurrentAPIVersion}/UsersAuth/register"
            }, 
            withBearer: false
            );
        }

		public async Task<T> LogoutAsync<T>(TokenDTO obj)
		{
			return await _baseService.SendAsync<T>(new Models.APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = obj,
				Url = villaUrl + $"/api/{SD.CurrentAPIVersion}/UsersAuth/revoke"
			});
		}
	}
        
}
