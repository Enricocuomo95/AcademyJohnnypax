namespace MarioKart.Repositoris
{
    public interface IRepository<T>
    {
        public IEnumerable<T> getAll();
        public T? getForName(String name);
        public bool Insert(T entity);
        public bool Update(T entity);
        public bool Delete(T entity);
    }
}
