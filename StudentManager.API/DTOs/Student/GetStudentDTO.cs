namespace StudentManager.API.DTOs.Student;

public record GetStudentDTO(
    int Id,
    string Name,
    int Age,
    int GradeId
);
