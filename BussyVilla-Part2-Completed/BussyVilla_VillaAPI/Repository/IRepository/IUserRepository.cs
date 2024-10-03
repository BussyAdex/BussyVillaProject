using BussyVilla_VillaAPI.Models;
using BussyVilla_VillaAPI.Models.Dto;

namespace BussyVilla_VillaAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<TokenDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO);
		Task<TokenDTO> RefreshAccessToken(TokenDTO tokentDTO);
        Task RevokeRefreshToken(TokenDTO tokentDTO);
	}
}
