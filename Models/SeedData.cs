using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcDiary.Data;

namespace MvcDiary.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any diaries.
                if (!context.Diaries.Any())
                {
                    context.Diaries.AddRange(
                        new Diary
                        {
                            Title = "Hello World",
                            UserId = "39be3681-7b73-4faa-af4a-05f86b03660d",
                            ReleaseDate = DateTime.Parse("2020-3-12"),
                            IsPublic = true,
                            Category = "Emotion",
                            Message = "欢迎，来到我的世界！"
                        });
                }
                else if (!context.Roots.Any())
                {
                    context.Roots.AddRange(
                        new Root
                        {
                            Name = "a-",
                            Category = "prefix",
                            Meaning = "加在单词或词根前面，表示“不，无，非”|加在单词前，表示“在…，…的”"
                        },
                        new Root
                        {
                            Name = "ab-,abs-",
                            Category = "prefix",
                            Meaning = "加在词根前，表示“相反，变坏，离去”等"
                        },
                        new Root
                        {
                            Name = "ab-,ac-,ad-,af-,ag-,an-,ap-,ar-,as-,at-",
                            Category = "prefix",
                            Meaning = "加在同辅音字母的词根前，表示“一再”等加强意"
                        },
                        new Root
                        {
                            Name = "ad-",
                            Category = "prefix",
                            Meaning = "加在在单词或词根前，表示“做…，加强…”"
                        },
                        new Root
                        {
                            Name = "amphi-",
                            Category = "prefix",
                            Meaning = "表示“两个，两种”"
                        },
                        new Root
                        {
                            Name = "an-",
                            Category = "prefix",
                            Meaning = "在词根前，表示“不，无”"
                        },
                        new Root
                        {
                            Name = "ana-",
                            Category = "prefix",
                            Meaning = "表示“错误，在旁边，分开”"
                        },
                        new Root
                        {
                            Name = "ante-",
                            Category = "prefix",
                            Meaning = "表示“前面，先”"
                        }
                        );
                }
                else
                {
                    return;
                }

                context.SaveChanges();
            }
        }
    }
}
