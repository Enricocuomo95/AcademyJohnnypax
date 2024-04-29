namespace aspWeb.Repositoris
{
    public interface IRepository<T>
    {
        public bool insert(T t);
        public T? getById(int id);
        public List<T> getAll();
        public bool delete(T t);
        public bool update(T t);
    }
}
