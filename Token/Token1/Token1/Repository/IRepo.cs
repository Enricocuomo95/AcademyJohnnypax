namespace Token1.Repository
{
    public interface IRepo<T>
    {
        public T GetValue(int id);
        public List<T> GetAll();
        public bool insertValue(T value);   
        public bool updateValue(T value);
        public bool deleteValue(int id);
    }
}
