namespace StudentManager.API.DTOs.Student;

public record UpdateStudentDTO(
    string Name,
    int Age,
    int GradeId
);
