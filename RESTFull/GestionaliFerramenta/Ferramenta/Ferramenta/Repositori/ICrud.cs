namespace Ferramenta.Repositori
{
    public interface ICrud <T>
    {
        //create
        public bool Insert(T entity);
        //read
        public List<T> getAll();
        //Update
        public bool UpdateValue(T entity);
        //delete
        public bool DeleteValue(T entity);
    }
}
