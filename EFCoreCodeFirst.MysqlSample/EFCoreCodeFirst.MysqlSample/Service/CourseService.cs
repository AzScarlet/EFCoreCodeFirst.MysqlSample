using EFCoreCodeFirst.MysqlSample.Model;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirst.MysqlSample.Service
{
    public class CourseService : ICrud<Course>
    {
        private CourseDbContext db;

        public CourseService(CourseDbContext? _db = null)
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

        public bool Create(Course data)
        {
            try
            {
                db.Courses.Add(data);
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
                    var datas = db.Courses.Where(x => x.Id == Id);
                    db.Courses.RemoveRange(datas);
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

        public Course? FindById(object id)
        {
            if (id is long key)
            {
                return db.Courses.Where(x => x.Id == key).FirstOrDefault();
            }
            return null;
        }

        public List<Course>? ReadAll()
        {
            return db.Courses.AsNoTracking().ToList();
        }

        public List<Course>? Search(object key)
        {
            if (key is long Id)
            {
                return db.Courses.Where(x => x.Id == Id).AsNoTracking().ToList();
            }

            if (key is string keyword)
            {
                keyword = keyword.ToLower();
                return db.Courses.Where(x => x.Name.ToLower().Contains(keyword) ||
                x.CourseCode.ToLower().Contains(keyword)).AsNoTracking().ToList();
            }

            return null;
        }

        public bool Update(Course data)
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
    }
}
