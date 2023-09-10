using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLab2.DAL.Data;
using WebLab2.DAL.Entities;

namespace WebLab2.Services
{
    public class DbInitializer
    {
        public static async Task Seed(ApplicationDbContext context,
                                      UserManager<ApplicationUser> userManager,
                                      RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();
            if (!context.Roles.Any())
            {
                var roleAdmin = new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "admin"
                };

                await roleManager.CreateAsync(roleAdmin);
            }

            if (!context.Users.Any())
            {
                var user = new ApplicationUser
                {
                    Email = "user@mail.ru",
                    UserName = "user@mail.ru"
                };
                await userManager.CreateAsync(user, "123456");

                var admin = new ApplicationUser
                {
                    Email = "admin@mail.ru",
                    UserName = "admin@mail.ru"
                };
                await userManager.CreateAsync(admin, "123456");
                admin = await userManager.FindByEmailAsync("admin@mail.ru");
                await userManager.AddToRoleAsync(admin, "admin");                
            }
            if (!context.TransportGroups.Any())
            {
                context.TransportGroups.AddRange(
                new List<TransportGroup>
                {
                     new TransportGroup {GroupName="Фантастический"},
                     new TransportGroup {GroupName="Специальный"},
                     new TransportGroup {GroupName="Красивый"},
                     new TransportGroup {GroupName="Быстрый"},
                     new TransportGroup {GroupName="Веселый"},
                     new TransportGroup {GroupName="Обычный"},
                });
                await context.SaveChangesAsync();
            }

            if (!context.Transports.Any())
            {
                context.Transports.AddRange(
                new List<Transport>
                {
                     new Transport {TransportName="Пагани",
                     Description="Описание пагани",
                     Price =16500, TransportGroupId=3, Image="Пагани.jpg" },
                     new Transport {TransportName="Линкор Айова",
                     Description="Описание линкора",
                     Price =100000, TransportGroupId=3, Image="Линкор_Айова.jpg" },
                     new Transport {TransportName="Черный дрозд",
                     Description="Описание дрозда",
                     Price =56000, TransportGroupId=4, Image="Черный_дрозд.jpg" },
                     new Transport {TransportName="НЛО",
                     Description="Описание нло",
                     Price =42, TransportGroupId=4, Image="НЛО.jpg" },
                     new Transport {TransportName="Черепаха",
                     Description="Описание черепахи",
                     Price =1, TransportGroupId=5, Image="Черепаха.jpg" },
                     new Transport {TransportName="Машина времени",
                     Description="Описание машины времени",
                     Price =90, TransportGroupId=5, Image="Машина_времени.jpg" },
                });
                await context.SaveChangesAsync();
            }
        }

        
    }
}
