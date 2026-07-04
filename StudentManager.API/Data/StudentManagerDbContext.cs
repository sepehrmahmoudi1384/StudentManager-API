using Microsoft.EntityFrameworkCore;
using StudentManager.API.Models;

namespace StudentManager.API.Data;

public class StudentManagerDbContext(
        DbContextOptions options
    ) 
    : DbContext(options)
{
    public DbSet<Student> Students => Set<Student>();

    public DbSet<Grade> Grades => Set<Grade>();
}
