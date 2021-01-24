using DocumentLibrary.Infrastructure.AspNetHelpers.Contracts;

namespace DocumentLibrary.Infrastructure.AspNetHelpers.UserService
{
    public class UserServiceStartupTime: IUserService
    {
        public string Username => "startup.time.user";
    }
}