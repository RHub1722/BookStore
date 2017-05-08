using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Entities
{
    public static class DbExtensions
    {
        public static void Seed(this IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<EFContext>();
            if (context.Books.Any())
                return;
            var categories = new []
            {
                new Category(){Name = "Science fiction" , ObjectState = ObjectState.Added},
                new Category(){Name = "Development", ObjectState = ObjectState.Added},
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();

            context.Users.AddRange(new []
            {
                new User(){Role = "Admin", ObjectState = ObjectState.Added},
                new User(){Role = "User", ObjectState = ObjectState.Added},
                new User(){Role = "User", ObjectState = ObjectState.Added},
            });
            context.SaveChanges();

            context.Books.AddRange(new []
            {
                new Book(){ChategoryId = categories[0].Id ,Name = "The Hunger Games", Author = "Suzanne Collins", Price = 100, ObjectState = ObjectState.Added}, 
                new Book(){ChategoryId = categories[0].Id ,Name = "Neuromancer", Author = "William Gibson", Price = 100, ObjectState = ObjectState.Added}, 
                new Book(){ChategoryId = categories[0].Id ,Name = "Johnny Mnemonic", Author = "William Gibson", Price = 100, ObjectState = ObjectState.Added},

                new Book(){ChategoryId = categories[1].Id ,Name = "CLR via C#", Author = "Jeffrey Richter", Price = 200, ObjectState = ObjectState.Added},
                new Book(){ChategoryId = categories[1].Id ,Name = "C# in Depth", Author = "Jon Skeet", Price = 200, ObjectState = ObjectState.Added},
            });
            context.SaveChanges();
        }
    }
}
