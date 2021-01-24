using Microsoft.AspNetCore.Http;
using System;
using DocumentLibrary.Infrastructure.AspNetHelpers.Contracts;

namespace DocumentLibrary.Infrastructure.AspNetHelpers.UserService
{
    public class UserService : IUserService
    {
        private string _username;

        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Username
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_username))
                {
                    _username = GetUsername();
                }
                
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        private string GetUsername()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
            {
                throw new Exception("The Http context is null");
            }

            // string username = httpContext.User?.Identity?.Name;
            string username = "no.auth.user";
            
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception("The current user username is null, empty or whitespace.");
            }

            return username;
        }
    }
}