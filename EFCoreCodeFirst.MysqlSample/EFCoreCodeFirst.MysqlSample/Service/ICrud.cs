namespace EFCoreCodeFirst.MysqlSample.Service
{
    public interface ICrud<T> where T : class
    {
        public bool Create(T data);
        public List<T>? ReadAll();
        public bool Update(T data);
        public bool Delete(object id);
        public T? FindById(object id);
        public List<T>? Search(object key);
    }
}
