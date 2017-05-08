using Microsoft.AspNetCore.Mvc;

namespace BookStore
{
    public class CustomAuthorize : TypeFilterAttribute
    {
        private readonly string[] _role;

        public CustomAuthorize(params string[] roles) : base(typeof(CustomAuthFilter))
        {
            _role = roles;
            Arguments = new[] { _role};
        }
    }
}
