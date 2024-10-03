using BussyVilla_Web.Models.Dto;

namespace BussyVilla_Web.Services.IServices
{
	public interface ITokenProvider
	{
		void SetToken(TokenDTO tokenDTO);
		TokenDTO? GetToken();
		void ClearToken();
	}
}
