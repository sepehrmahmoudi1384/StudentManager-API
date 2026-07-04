using Microsoft.EntityFrameworkCore;
using StudentManager.API.Models;

namespace StudentManager.API.Data;

public static class DataExtensions
{
    public static void MigrateDb(
        this WebApplication app
    )
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider
            .GetRequiredService<StudentManagerDbContext>();
        dbContext.Database.Migrate();
    }

    public static void AddStudentManagerDb(
        this WebApplicationBuilder builder
    )
    {
        builder.Services.AddSqlite<StudentManagerDbContext>(
            connectionString: builder.Configuration
                                .GetConnectionString("StudentManagerDb"),
            optionsAction: options => options.UseSeeding(
                (context, _) =>
                {
                    if (!context.Set<Grade>().Any())
                    {
                        context.Set<Grade>().AddRange(
                            new Grade {Name = "First"},
                            new Grade {Name = "Second"},
                            new Grade {Name = "Third"},
                            new Grade {Name = "Fourth"},
                            new Grade {Name = "Fifth"},
                            new Grade {Name = "Sixth"},
                            new Grade {Name = "Seventh"},
                            new Grade {Name = "Eighth"},
                            new Grade {Name = "Ninth"},
                            new Grade {Name = "Tenth"},
                            new Grade {Name = "Eleventh"},
                            new Grade {Name = "Twelfth"}
                        );

                        context.SaveChanges();
                    }
                }
            )    
        );
    }
}
