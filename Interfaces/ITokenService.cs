using Iu_InstaShare_Api.Models;

namespace Iu_InstaShare_Api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(UserProfileModel user);
    }
}
