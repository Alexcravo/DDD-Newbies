using Hdn.Rewards.Domain.DTO;
using Hdn.Rewards.Domain.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace Hdn.Rewards.Domain.Interfaces.Business
{
    public interface ILoginBusiness
    {
        TokenDto ValidateCredentials(LoginRequest login);
        TokenDto ValidadeCredentials(TokenDto token);

        void UpdateLastAccess(User user);

        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
