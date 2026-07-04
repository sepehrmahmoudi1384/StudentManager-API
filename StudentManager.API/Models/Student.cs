using System.ComponentModel.DataAnnotations;

namespace StudentManager.API.Models;

public class Student
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }

    [Required]
    [Range(7, 150)]
    public int Age { get; set; }

    [Required]
    public int GradeId { get; set; }

    public Grade? Grade { get; set; }
}
