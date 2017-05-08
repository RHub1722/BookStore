using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace BookStore
{
    public class CustomAuthFilter: IAsyncActionFilter
    {
        private readonly string[] _roles;
        private readonly EFContext _context;

        public CustomAuthFilter(string[] roles, EFContext context)
        {
            _roles = roles;
            _context = context;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            KeyValuePair<string, StringValues> user = context.HttpContext.Request.Headers.FirstOrDefault(x =>x.Key == "userid");
            if (user.Equals(default(KeyValuePair<string, StringValues>)))
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                int headId;
                if (!int.TryParse(user.Value.ToString(), out headId))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                if (_roles == null || !_roles.Any())
                {
                    await next();
                    return;
                }

                var userDb = _context.Users.FirstOrDefault(x =>x.Id == headId);
                if (userDb == null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                if (_roles.Any() &&!_roles.Contains(userDb.Role))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                await next();
            }
            
        }
    }
}
