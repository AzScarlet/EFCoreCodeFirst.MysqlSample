using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreCodeFirst.MysqlSample.Model
{
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
}
