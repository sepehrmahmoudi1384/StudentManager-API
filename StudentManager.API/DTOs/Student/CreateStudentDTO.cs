namespace StudentManager.API.DTOs.Student;

public record CreateStudentDTO(
    string Name,
    int Age,
    int GradeId
);
