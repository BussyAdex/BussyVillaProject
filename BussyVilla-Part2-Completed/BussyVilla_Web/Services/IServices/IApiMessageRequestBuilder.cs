using BussyVilla_Web.Models;

namespace BussyVilla_Web.Services.IServices
{
	public interface IApiMessageRequestBuilder
	{
		HttpRequestMessage Build(APIRequest apiRequest);
	}
}
