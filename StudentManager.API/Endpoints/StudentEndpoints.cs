using Microsoft.EntityFrameworkCore;
using StudentManager.API.Data;
using StudentManager.API.DTOs.Student;
using StudentManager.API.Models;

namespace StudentManager.API.Endpoints;

public static class StudentEndpoints
{
    const string GetStudentEndpointName = "GetStudent";
    public static void MapStudentEndPoints(
        this WebApplication app
    )
    {
        var studentGroup = app.MapGroup("/students");

        // GET /students
        studentGroup.MapGet("/", async (StudentManagerDbContext dbContext) =>
        {
            var students = await dbContext.Set<Student>()
                .Include(student => student.Grade)
                .Select(student => new GetAllStudentDTO(
                    student.Id,
                    student.Name,
                    student.Age,
                    student.Grade!.Name
                ))
                .AsNoTracking()
                .ToListAsync();

            return Results.Ok(students);
        });

        // GET /students/1
        studentGroup.MapGet(
            "/{id}",
            async (int id, StudentManagerDbContext dbContext) =>
            {
                var student = await dbContext.Set<Student>()
                    .FindAsync(id);

                return student is null
                    ? Results.NotFound()
                    : Results.Ok(new GetStudentDTO(student.Id, student.Name, student.Age, student.GradeId));
            }
        ).WithName(GetStudentEndpointName);

        // POST /students
        studentGroup.MapPost(
            "/",
            async (
                CreateStudentDTO newStudent,
                StudentManagerDbContext dbContext
            ) =>
        {
            var student = new Student
            {
                Name = newStudent.Name,
                Age = newStudent.Age,
                GradeId = newStudent.GradeId
            };

            await dbContext.Set<Student>()
                .AddAsync(student);

            await dbContext.SaveChangesAsync();

            var studentDto = new GetStudentDTO(
                student.Id,
                student.Name,
                student.Age,
                student.GradeId
            );

            return Results
                .CreatedAtRoute(GetStudentEndpointName, new { id = studentDto.Id }, studentDto);
        });

        // PUT /students/1
        studentGroup.MapPut(
            "/{id}",
            async (
                int id,
                UpdateStudentDTO newStudent,
                StudentManagerDbContext dbContext
            ) =>
            {
                var existingStudent = await dbContext.Students.FindAsync(id);

                if (existingStudent is null)
                    return Results.NotFound();

                (existingStudent.Name, existingStudent.Age, existingStudent.GradeId) =
                    (newStudent.Name, newStudent.Age, newStudent.GradeId);
                
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            }
        );

        // DELETE /students/1
        studentGroup.MapDelete(
            "/{id}",
            async (int id, StudentManagerDbContext dbContext) =>
            {
                var student = await dbContext.Students.FindAsync(id);
                
                if (student is null)
                    return Results.NotFound();

                dbContext.Students.Remove(student);
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            }
        );
    }
}
