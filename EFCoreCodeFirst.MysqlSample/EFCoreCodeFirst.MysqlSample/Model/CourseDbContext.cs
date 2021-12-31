using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirst.MysqlSample.Model
{
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
}
