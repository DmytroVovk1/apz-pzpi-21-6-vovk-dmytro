using Discerniy.Domain.Entity.DomainEntity;
using Discerniy.Domain.Requests;
using Discerniy.Domain.Responses;
using Microsoft.AspNetCore.Http;

namespace Discerniy.Domain.Interface.Services
{
    public interface IAuthService
    {
        void SetHttpContext(HttpContext httpContext);
        Task<TokenResponse> Login(LoginModelRequest request);
        Task<TokenResponse> GenerateDeviceToken(string userId);
        Task<TokenResponse> RefreshDeviceToken();
        Task<string> Refresh();
        Task<UserModel> GetUserByDevice();
        Task<UserModel> GetUser();
        string GetClientId();
        string GetClientSessionId();
    }
}
