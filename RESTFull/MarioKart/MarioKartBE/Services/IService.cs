namespace MarioKart.Services
{
    public interface IService<T>
    {
        public IEnumerable<T> getAll();
        public T getByName(String name);
        public bool Create(T entity);
        public bool Update(T entity);
        public bool Delete(T entity);


    }
}
