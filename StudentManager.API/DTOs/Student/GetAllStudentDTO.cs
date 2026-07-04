namespace StudentManager.API.DTOs.Student;

public record GetAllStudentDTO(
    int Id,
    string Name,
    int Age,
    string GradeName
);
