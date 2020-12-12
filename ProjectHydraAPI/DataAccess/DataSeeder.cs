using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectHydraAPI.Helpers;
using ProjectHydraAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHydraAPI.DataAccess
{
    public static class DataSeeder
    {
        public static void Seed(IServiceProvider service)
        {
            var context = service.GetRequiredService<HydraDbContext>();
            var roleManger = service.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = service.GetRequiredService <UserManager<AppUser>>();

            //CREATE ROLES
            if (!roleManger.Roles.Any())
            {
                var role = new IdentityRole
                {
                    Id = 1.ToString(),
                    Name = RolesStrings.Admin,
                    NormalizedName = RolesStrings.Admin.ToUpper()
                };

                var task = roleManger.CreateAsync(role);
                task.Wait();
                role = new IdentityRole
                {
                    Id = 2.ToString(),
                    Name = RolesStrings.User,
                    NormalizedName = RolesStrings.User.ToUpper()
                };
                task = roleManger.CreateAsync(role);
                task.Wait();
            }

            //SEED EVERYTHING ELSE
            using (var transaction = context.Database.BeginTransaction())
            {
                if (!context.Ranks.Any())
                {
                    var ranks = new List<Rank>
                {
            new Rank
            {
                Id = 1,
                Hierarchy = 1,
                Name = "Szeregowy"
            },
            new Rank
            {
                Id = 2,
                Hierarchy = 2,
                Name = "Starszy szeregowy"
            },
            new Rank
            {
                Id = 3,
                Hierarchy = 3,
                Name = "Kapral"
            },
            new Rank
            {
                Id = 4,
                Hierarchy = 4,
                Name = "Starszy kapral"
            },
            new Rank
            {
                Id = 5,
                Hierarchy = 5,
                Name = "Plutonowy"
            },
            new Rank
            {
                Id = 6,
                Hierarchy = 6,
                Name = "Sierżant"
            },
            new Rank
            {
                Id = 7,
                Hierarchy = 7,
                Name = "Starszy sierżant"
            },
            new Rank
            {
                Id = 8,
                Hierarchy = 8,
                Name = "Młodszy chorąży"
            },
            new Rank
            {
                Id = 9,
                Hierarchy = 9,
                Name = "Chorąży"
            },
            new Rank
            {
                Id = 10,
                Hierarchy = 10,
                Name = "Starszy chorąży"
            },
            new Rank
            {
                Id = 11,
                Hierarchy = 11,
                Name = "Starszy chorąży sztabowy"
            },
            new Rank
            {
                Id = 12,
                Hierarchy = 12,
                Name = "Podporucznik"
            },
            new Rank
            {
                Id = 13,
                Hierarchy = 13,
                Name = "Porucznik"
            },
            new Rank
            {
                Id = 14,
                Hierarchy = 14,
                Name = "Kapitan"
            },
            new Rank
            {
                Id = 15,
                Hierarchy = 15,
                Name = "Major"
            },
            new Rank
            {
                Id = 16,
                Hierarchy = 16,
                Name = "Podpułkownik"
            },
            new Rank
            {
                Id = 17,
                Hierarchy = 17,
                Name = "Pułkownik"
            },
            new Rank
            {
                Id = 18,
                Hierarchy = 18,
                Name = "Generał brygady"
            },
            new Rank
            {
                Id = 19,
                Hierarchy = 19,
                Name = "Generał dywizji"
            },
            new Rank
            {
                Id = 20,
                Hierarchy = 20,
                Name = "Generał broni"
            },
            new Rank
            {
                Id = 21,
                Hierarchy = 21,
                Name = "Generał"
            }
                };
                    context.Ranks.AddRange(ranks);
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Ranks ON");
                    context.SaveChanges();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Ranks OFF");
                }
                if (!context.Units.Any())
                {
                    var units = new List<Unit>
                {
                    new Unit
                {
                    Id = 1,
                    Name = "Batalion piechoty",
                },
                new Unit
                {
                    Id = 2,
                    Name = "Kompania dowodzenia i zabezpieczenia",
                    ParentId = 1
                },
                new Unit
                {
                    Id = 3,
                    Name = "1 Kompania zmechanizowana",
                    ParentId = 1,
                },
                new Unit
                {
                    Id = 4,
                    Name = "2 Kompania zmechanizowana",
                    ParentId = 1
                },
                new Unit
                {
                    Id = 5,
                    Name = "1 Pluton zmechanizowany",
                    ParentId = 3,
                },
                new Unit
                {
                    Id = 6,
                    Name = "2 Pluton zmechanizowany",
                    ParentId = 3,
                },
                new Unit
                {
                    Id = 7,
                    Name = "1 Drużyna zmechanizowana",
                    ParentId = 5,
                },
                new Unit
                {
                    Id = 8,
                    Name = "2 Drużyna zmechanizowana",
                    ParentId = 5,
                }
                };
                    context.Units.AddRange(units);
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Units ON");
                    context.SaveChanges();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Units OFF");

                }

                if (!context.Users.Any())
                {
                    var hasher = new PasswordHasher<AppUser>();
                    var users = new List<AppUser>
                {
                    new AppUser
                {
                    Id = "aeb60b01-3d2a-4438-9621-42b0a6c8ed18",
                    UserName = "admin@admin.pl",
                    Email = "admin@admin.pl",
                    NormalizedEmail = "ADMIN@ADMIN.PL",
                    NormalizedUserName = "ADMIN@ADMIN.PL",
                    PasswordHash = hasher.HashPassword(null, "Admin12"),
                    FirstName = "Jan",
                    LastName = "Ślusarz",
                    PhoneNumber = "785855478",
                    Birthday = new System.DateTime(1985, 2, 19),
                    RankId = 17,
                    UnitId = 1
                },
                new AppUser
                {
                    Id = "e5c936bc-20c3-47d5-822e-316a7d73e7e5",
                    UserName = "test1@test.pl",
                    Email = "test1@test.pl",
                    NormalizedEmail = "TEST1@TEST.PL",
                    NormalizedUserName = "TEST1@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Michał",
                    LastName = "Jarosławski",
                    PhoneNumber = "28468452",
                    Birthday = new System.DateTime(1995, 10, 5),
                    RankId = 14,
                    UnitId = 3
                },
                new AppUser
                {
                    Id = "2a4c3136-c887-4ff1-9597-1bf4db8dd0cb",
                    UserName = "test2@test.pl",
                    Email = "test2@test.pl",
                    NormalizedEmail = "TEST2@TEST.PL",
                    NormalizedUserName = "TEST2@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Janusz",
                    LastName = "Michalski",
                    PhoneNumber = "605278397",
                    Birthday = new System.DateTime(1990, 9, 26),
                    RankId = 13,
                    UnitId = 5
                },
                new AppUser
                {
                    Id = "037a812e-0503-4677-b8d8-7afebac4339e",
                    UserName = "test3@test.pl",
                    Email = "test3@test.pl",
                    NormalizedEmail = "TEST3@TEST.PL",
                    NormalizedUserName = "TEST3@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Walenty",
                    LastName = "Świerzbowski",
                    PhoneNumber = "593608309",
                    Birthday = new System.DateTime(1978, 3, 24),
                    RankId = 5,
                    UnitId = 7
                },
                new AppUser
                {
                    Id = "85e3e69c-4bd7-4d18-a38e-8f871ae14e4e",
                    UserName = "test4@test.pl",
                    Email = "test4@test.pl",
                    NormalizedEmail = "TEST4@TEST.PL",
                    NormalizedUserName = "TEST4@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Piotr",
                    LastName = "Pierzyna",
                    PhoneNumber = "482957333",
                    Birthday = new System.DateTime(1988, 6, 3),
                    RankId = 2,
                    UnitId = 7
                },
                new AppUser
                {
                    Id = "252f85d5-3987-483d-bf82-3f7741c30e34",
                    UserName = "test5@test.pl",
                    Email = "test5@test.pl",
                    NormalizedEmail = "TEST5@TEST.PL",
                    NormalizedUserName = "TEST5@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Filip",
                    LastName = "Paderewski",
                    PhoneNumber = "566793009",
                    Birthday = new System.DateTime(1984, 12, 3),
                    RankId = 1,
                    UnitId = 7
                },
                new AppUser
                {
                    Id = "17591e7b-62d3-4b62-9542-83a534ae4ee9",
                    UserName = "test6@test.pl",
                    Email = "test6@test.pl",
                    NormalizedEmail = "TEST6@TEST.PL",
                    NormalizedUserName = "TEST6@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Andrzej",
                    LastName = "Osoński",
                    PhoneNumber = "683920005",
                    Birthday = new System.DateTime(1987, 1, 30),
                    RankId = 2,
                    UnitId = 6
                },
                new AppUser
                {
                    Id = "752aa4a9-694e-4269-a528-0d4b05772afe",
                    UserName = "test7@test.pl",
                    Email = "test7@test.pl",
                    NormalizedEmail = "TEST7@TEST.PL",
                    NormalizedUserName = "TEST7@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Sławomir",
                    LastName = "Celsiński",
                    PhoneNumber = "478999345",
                    Birthday = new System.DateTime(1989, 7, 14),
                    RankId = 1,
                    UnitId = 7
                },
                new AppUser
                {
                    Id = "53b99ab6-cb84-4818-a131-ad0d61351490",
                    UserName = "test8@test.pl",
                    Email = "test8@test.pl",
                    NormalizedEmail = "TEST8@TEST.PL",
                    NormalizedUserName = "TEST8@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Tadeusz",
                    LastName = "Krajniak",
                    PhoneNumber = "678432456",
                    Birthday = new System.DateTime(1985, 5, 9),
                    RankId = 1,
                    UnitId = 7
                },
                new AppUser
                {
                    Id = "02c7089f-22db-471d-a4f3-5cf9133b6229",
                    UserName = "test9@test.pl",
                    Email = "test9@test.pl",
                    NormalizedEmail = "TEST9@TEST.PL",
                    NormalizedUserName = "TEST9@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Karol",
                    LastName = "Raborski",
                    PhoneNumber = "841536497",
                    Birthday = new System.DateTime(1991, 4, 19),
                    RankId = 5,
                    UnitId = 7
                },
                new AppUser
                {
                    Id = "923a3751-8efc-41e6-871b-c72aa4377242",
                    UserName = "test10@test.pl",
                    Email = "test10@test.pl",
                    NormalizedEmail = "TEST10@TEST.PL",
                    NormalizedUserName = "TEST10@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Sebastian",
                    LastName = "Rejewski",
                    PhoneNumber = "747983003",
                    Birthday = new System.DateTime(1990, 3, 25),
                    RankId = 3,
                    UnitId = 7
                },
                new AppUser
                {
                    Id = "41676574-bae2-4085-bb56-1b419298d5a6",
                    UserName = "test11@test.pl",
                    Email = "test11@test.pl",
                    NormalizedEmail = "TEST11@TEST.PL",
                    NormalizedUserName = "TEST11@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Przemysław",
                    LastName = "Czubowski",
                    PhoneNumber = "969247336",
                    Birthday = new System.DateTime(1988, 11, 11),
                    RankId = 4,
                    UnitId = 7
                },
                new AppUser
                {
                    Id = "25d50227-760a-4067-84e7-98eb6af9146f",
                    UserName = "test12@test.pl",
                    Email = "test12@test.pl",
                    NormalizedEmail = "TEST12@TEST.PL",
                    NormalizedUserName = "TEST12@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Daniel",
                    LastName = "Kałużny",
                    PhoneNumber = "749107186",
                    Birthday = new System.DateTime(1980, 8, 30),
                    RankId = 6,
                    UnitId = 7
                },
                new AppUser
                {
                    Id = "cba07709-c439-4ffb-bf19-22e3e01fc46e",
                    UserName = "test13@test.pl",
                    Email = "test13@test.pl",
                    NormalizedEmail = "TEST13@TEST.PL",
                    NormalizedUserName = "TEST13@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Maciek",
                    LastName = "Janecki",
                    PhoneNumber = "698356568",
                    Birthday = new System.DateTime(1987, 12, 30),
                    RankId = 5,
                    UnitId = 7
                },
                new AppUser
                {
                    Id = "34f0bd45-e05d-4bba-825e-2316ea095371",
                    UserName = "test14@test.pl",
                    Email = "test14@test.pl",
                    NormalizedEmail = "TEST14@TEST.PL",
                    NormalizedUserName = "TEST14@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Patryk",
                    LastName = "Szostak",
                    PhoneNumber = "497467756",
                    Birthday = new System.DateTime(1987, 2, 24),
                    RankId = 4,
                    UnitId = 7
                },
                new AppUser
                {
                    Id = "33b45fcb-18f6-4b65-8a8a-ac6d81284609",
                    UserName = "test15@test.pl",
                    Email = "test15@test.pl",
                    NormalizedEmail = "TEST15@TEST.PL",
                    NormalizedUserName = "TEST15@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Wojciech",
                    LastName = "Czacharski",
                    PhoneNumber = "336995111",
                    Birthday = new System.DateTime(1984, 6, 18),
                    RankId = 3,
                    UnitId = 7
                },
                new AppUser
                {
                    Id = "e5cd10b9-2cd3-4c78-93d1-d26367aa7932",
                    UserName = "test16@test.pl",
                    Email = "test16@test.pl",
                    NormalizedEmail = "TEST16@TEST.PL",
                    NormalizedUserName = "TEST16@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Eryk",
                    LastName = "Krempucki",
                    PhoneNumber = "798456323",
                    Birthday = new System.DateTime(1989, 4, 9),
                    RankId = 2,
                    UnitId = 7
                },
                new AppUser
                {
                    Id = "055aadd3-049e-4567-b4e2-780debfad121",
                    UserName = "test17@test.pl",
                    Email = "test17@test.pl",
                    NormalizedEmail = "TEST17@TEST.PL",
                    NormalizedUserName = "TEST17@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Mirosław",
                    LastName = "Udempski",
                    PhoneNumber = "764947236",
                    Birthday = new System.DateTime(1980, 3, 24),
                    RankId = 1,
                    UnitId = 7
                },
                new AppUser
                {
                    Id = "053d355c7-7e67-47c1-b8d1-8e8d354f9b30",
                    UserName = "test18@test.pl",
                    Email = "test18@test.pl",
                    NormalizedEmail = "TEST18@TEST.PL",
                    NormalizedUserName = "TEST18@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Tomasz",
                    LastName = "Grampski",
                    PhoneNumber = "346853008",
                    Birthday = new System.DateTime(1986, 9, 20),
                    RankId = 4,
                    UnitId = 7
                },
                new AppUser
                {
                    Id = "0096c46a-1810-4458-a460-c60ea86c8a06",
                    UserName = "test19@test.pl",
                    Email = "test19@test.pl",
                    NormalizedEmail = "TEST19@TEST.PL",
                    NormalizedUserName = "TEST19@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Jakub",
                    LastName = "Bochenek",
                    PhoneNumber = "006390778",
                    Birthday = new System.DateTime(1984, 5, 19),
                    RankId = 8,
                    UnitId = 5
                },
                new AppUser
                {
                    Id = "586f6f08-7cb8-49f4-80f7-d64793073bc9",
                    UserName = "test20@test.pl",
                    Email = "test20@test.pl",
                    NormalizedEmail = "TEST20@TEST.PL",
                    NormalizedUserName = "TEST20@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Cyprian",
                    LastName = "Muller",
                    PhoneNumber = "578345554",
                    Birthday = new System.DateTime(1992, 1, 21),
                    RankId = 12,
                    UnitId = 3
                },
                new AppUser
                {
                    Id = "b4bd5550-664c-44e9-b09e-c88f4dbc9598",
                    UserName = "test21@test.pl",
                    Email = "test21@test.pl",
                    NormalizedEmail = "TEST21@TEST.PL",
                    NormalizedUserName = "TEST21@TEST.PL",
                    PasswordHash = hasher.HashPassword(null, "Test123"),
                    FirstName = "Kacper",
                    LastName = "Wesołowski",
                    PhoneNumber = "567834990",
                    Birthday = new System.DateTime(1989, 5, 30),
                    RankId = 16,
                    UnitId = 1
                }
                };
                    context.Users.AddRange(users);
                    context.SaveChanges();
                }
                transaction.Commit();
            }
            if (context.Users.Any() && !context.UserRoles.Any())
            {

                foreach(var user in context.Users.ToList())
                {
                    if (user.Id == "aeb60b01-3d2a-4438-9621-42b0a6c8ed18")
                    {
                        var task = userManager.AddToRoleAsync(user, RolesStrings.Admin);
                        task.Wait();
                    }
                    else
                    {
                        var task = userManager.AddToRoleAsync(user, RolesStrings.User);
                        task.Wait();
                    }
                }
            }

            if(context.Units.Any() && context.Units.Any())
            {
                var unit = context.Units.Find(1);
                unit.CommanderId = "aeb60b01-3d2a-4438-9621-42b0a6c8ed18";
                unit.DeputyCommanderId = "b4bd5550-664c-44e9-b09e-c88f4dbc9598";
                context.SaveChanges();

                unit = context.Units.Find(3);
                unit.CommanderId = "e5c936bc-20c3-47d5-822e-316a7d73e7e5";
                unit.DeputyCommanderId = "586f6f08-7cb8-49f4-80f7-d64793073bc9";
                context.SaveChanges();

                unit = context.Units.Find(5);
                unit.CommanderId = "2a4c3136-c887-4ff1-9597-1bf4db8dd0cb";
                unit.DeputyCommanderId = "0096c46a-1810-4458-a460-c60ea86c8a06";
                context.SaveChanges();

                unit = context.Units.Find(7);
                unit.CommanderId = "037a812e-0503-4677-b8d8-7afebac4339e";
                unit.DeputyCommanderId = "02c7089f-22db-471d-a4f3-5cf9133b6229";
                context.SaveChanges();
            }

            if (context.Users.Any() && !context.Classes.Any())
            {
                var lessons = new List<Class>
                {
                    new Class
                    {
                        Topic = "Teoria BLOS",
                        TeacherId = "aeb60b01-3d2a-4438-9621-42b0a6c8ed18",
                        UnitId = 7,
                        Place = "Siedziba jednostki",
                        Date = DateTime.Now.AddDays(4),
                        Comment = "Proszę zabrać zeszyt i długopis",
                        Time = 2
                    },
                    new Class
                    {
                        Topic = "Taktyka zielona",
                        TeacherId = "586f6f08-7cb8-49f4-80f7-d64793073bc9",
                        UnitId = 5,
                        Place = "Strzelnica jednostki",
                        Date = DateTime.Now.AddDays(5),
                        Comment = "Wyposażenie taktyczne",
                        Time = 5
                    },
                    new Class
                    {
                        Topic = "Test sprawnościowy",
                        TeacherId = "0096c46a-1810-4458-a460-c60ea86c8a06",
                        UnitId = 5,
                        Place = "Hala sportowa",
                        Date = DateTime.Now.AddDays(-3),
                        Comment = "Strój sportowy",
                        Time = 3
                    },

                };
                context.Classes.AddRange(lessons);
                context.SaveChanges();
            }

            if(context.Users.Any() && !context.Grades.Any())
            {

                var grade = new Grade()
                {
                    LessonId = context.Classes.Where(c => c.Topic == "Test sprawnościowy").FirstOrDefault().Id,
                    UserId = "752aa4a9-694e-4269-a528-0d4b05772afe",
                    GradeNumber = 4,
                    Comment = "Pompki:50, Przysiady:60, Brzuszki: 45"
                };
                context.Grades.Add(grade);
                context.SaveChanges();
            }

        }
    }
}
