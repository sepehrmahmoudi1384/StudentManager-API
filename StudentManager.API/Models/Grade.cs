using System.ComponentModel.DataAnnotations;

namespace StudentManager.API.Models;

public class Grade
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(20)]
    public required string Name { get; set; }
}
