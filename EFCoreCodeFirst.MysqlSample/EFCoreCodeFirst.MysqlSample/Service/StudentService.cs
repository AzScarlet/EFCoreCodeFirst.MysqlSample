using EFCoreCodeFirst.MysqlSample.Model;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirst.MysqlSample.Service
{
    public class StudentService : ICrud<Student>
    {
        private CourseDbContext db;

        public StudentService(CourseDbContext? _db = null)
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

        public bool Create(Student data)
        {
            try
            {
                db.Students.Add(data);
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
                    var datas = db.Students.Where(x => x.Id == Id);
                    db.Students.RemoveRange(datas);
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

        public Student? FindById(object id)
        {
            if (id is long key)
            {
                return db.Students.Where(x => x.Id == key).FirstOrDefault();
            }
            return null;
        }

        public List<Student>? ReadAll()
        {
            return db.Students.AsNoTracking().ToList();
        }

        public List<Student>? Search(object key)
        {
            if (key is long Id)
            {
                return db.Students.Where(x => x.Id == Id).AsNoTracking().ToList();
            }

            if (key is string keyword)
            {
                keyword = keyword.ToLower();
                return db.Students.Where(x => x.Name.ToLower().Contains(keyword) ||
                x.Email.ToLower().Contains(keyword) || x.PhoneNumber.Contains(keyword))
                    .AsNoTracking().ToList();
            }

            return null;
        }

        public bool Update(Student data)
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
