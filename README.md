# Entity Framework Core Code-First
This is simple project to create database using Entity Framework Core Code-First approach
This sample is using Mysql database
## Getting Setup
Install Entity Framework tools on your system
```bash
dotnet tool install --global-ef
```
Install `Microsoft.EntityFrameworkCore` and `Pomelo.EntityFrameworkCore.MySql` from Nuget packages on your project
## Create Model
In this sample had three model
```csharp    
public partial class Course
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Required]
    public string CourseCode { get; set; }
    [Required]
    public string Name { get; set; }
}
    
public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public string? Address { get; set; }
}

public class StudentCourseRelation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Required]
    public long CourseId { get; set; }
    public virtual Course Course { get; set; }
    [Required]
    public long StudentId { get; set; }
    public virtual Student Student { get; set; }
}
```

## Create DBContext
```csharp
public class CourseDbContext : DbContext
{
    // connection string
    readonly string conn = "server=localhost;port=3306;user=root;password=1010;database=coursedb";
    readonly string db_version = "8.0.17-mysql";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(conn, ServerVersion.Parse(db_version));
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<StudentCourseRelation> StudentCourseRelations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(e =>
            e.HasIndex(i => i.CourseCode)
            .IsUnique()
        );
    }
}
```

## Create database using command line
Execute this command with cmd or terminal in project directory
### 1. Create first migration schema
```bash
dotnet ef migrations add InitDB
```
The word `InitDB` is the name of migration name schema
### 2. Apply schema to database
```bash
dotnet ef database update
```

## Migration
If you want to change database model, just change the model and add new migration schema. after that apply the schema
