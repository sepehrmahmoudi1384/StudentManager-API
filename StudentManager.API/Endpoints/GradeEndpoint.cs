using Microsoft.EntityFrameworkCore;
using StudentManager.API.Data;
using StudentManager.API.DTOs.Grade;
using StudentManager.API.Models;

namespace StudentManager.API.Endpoints;

public static class GradeEndpoint
{
    const string GetGradeEndpointName = "GetGrade";

    public static void MapGradeEndpoints(
        this WebApplication app
    )
    {
        var gradeGroup = app.MapGroup("/grades");

        // GET /grades
        gradeGroup.MapGet(
            "/",
            async (StudentManagerDbContext dbContext) =>
                await dbContext.Grades
                    .Select(grade => new GetAllGradeDto(grade.Id, grade.Name))
                    .AsNoTracking()
                    .ToListAsync()
        );

        // GET /grades/1
        gradeGroup.MapGet(
            "/{id}",
            async (int id, StudentManagerDbContext dbContext) =>
            {
                var grade = await dbContext.Set<Grade>().FindAsync(id);

                if (grade is null)
                    return Results.NotFound();

                var gradeDto = new GetGradeDto(grade.Id, grade.Name);

                return Results.Ok(gradeDto);
            }
        ).WithName(GetGradeEndpointName);

        // POST /grades
        gradeGroup.MapPost(
            "/",
            async (CreateGradeDto newGrade, StudentManagerDbContext dbContext) =>
            {
                var grade = new Grade
                {
                    Name = newGrade.Name
                };

                await dbContext.Grades.AddAsync(grade);
                await dbContext.SaveChangesAsync();

                return Results.CreatedAtRoute(
                    GetGradeEndpointName,
                    new { id = grade.Id },
                    grade
                );
            }
        );

        // PUT /grades/1
        gradeGroup.MapPut(
            "/{id}",
            async (
                int id,
                UpdateGradeDto newGrade,
                StudentManagerDbContext dbContext
            ) =>
            {
                var grade = await dbContext.Set<Grade>().FindAsync(id);

                if (grade is null)
                    return Results.NotFound();

                grade.Name = newGrade.Name;

                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            }
        );

        // DELETE /grades/1
        gradeGroup.MapDelete(
            "/{id}",
            async (
                int id,
                StudentManagerDbContext dbContext
            ) =>
            {
                var grade = await dbContext.Grades.FindAsync(id);

                if (grade is null)
                    return Results.NotFound();

                dbContext.Grades.Remove(grade);
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            }
        );
    }
}
