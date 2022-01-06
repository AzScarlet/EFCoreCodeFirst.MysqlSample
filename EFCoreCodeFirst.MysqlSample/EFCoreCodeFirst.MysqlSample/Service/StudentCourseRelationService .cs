using EFCoreCodeFirst.MysqlSample.Model;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirst.MysqlSample.Service
{
    public class StudentCourseRelationService : ICrud<StudentCourseRelation>
    {
        private CourseDbContext db;

        public StudentCourseRelationService(CourseDbContext? _db = null)
        {
            if (_db is null)
            {
                db = new();
            }
            else
            {
                db = _db;
            }
        }

        public bool Create(StudentCourseRelation data)
        {
            try
            {
                var exist = db.StudentCourseRelations.Any(x => x.StudentId == data.StudentId
                && x.CourseId == data.CourseId);
                if (exist)
                {
                    return false;
                }

                db.StudentCourseRelations.Add(data);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(object id)
        {
            try
            {
                if(id is long Id)
                {
                    var datas = db.StudentCourseRelations.Where(x => x.Id == Id);
                    db.StudentCourseRelations.RemoveRange(datas);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public StudentCourseRelation? FindById(object id)
        {
            if (id is long key)
            {
                return db.StudentCourseRelations.Where(x => x.Id == key).FirstOrDefault();
            }
            return null;
        }

        public List<StudentCourseRelation>? ReadAll()
        {
            return db.StudentCourseRelations.AsNoTracking().ToList();
        }

        public List<StudentCourseRelation>? Search(object key)
        {
            if (key is long Id)
            {
                return db.StudentCourseRelations.Where(x => x.Id == Id ||
                x.CourseId == Id || x.StudentId == Id).AsNoTracking()
                .ToList();
            }
            return null;
        }

        public bool Update(StudentCourseRelation data)
        {
            try
            {
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Course>? CoursesTaken(long studentId)
        {
            try
            {
                var courses = from x in db.Courses
                              join y in db.StudentCourseRelations
                              on x.Id equals y.CourseId
                              select x;
                return courses.AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool DeleteByStudentandCourseId(long courseId, long studentId)
        {
            try
            {
                var datas = db.StudentCourseRelations.Where(x => x.CourseId == courseId
                && x.StudentId == studentId);
                db.StudentCourseRelations.RemoveRange(datas);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
