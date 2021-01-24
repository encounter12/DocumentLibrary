using DocumentLibrary.Infrastructure.AspNetHelpers.Contracts;

namespace DocumentLibrary.Infrastructure.AspNetHelpers.UserService
{
    public class UserServiceDesignTime : IUserService
    {
        public string Username => "design.time.user";
    }
}