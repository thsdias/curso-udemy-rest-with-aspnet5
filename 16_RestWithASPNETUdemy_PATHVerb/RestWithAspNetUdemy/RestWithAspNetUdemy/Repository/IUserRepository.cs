using RestWithAspNetUdemy.Data.VO;
using RestWithAspNetUdemy.Model;

namespace RestWithAspNetUdemy.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
        
        User ValidateCredentials(string userName);

        User RefreshUserInfo(User user);

        bool RevokeToken(string userName);
    }
}
