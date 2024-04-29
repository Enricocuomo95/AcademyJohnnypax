namespace GestionaleOfficina.Repository
{
    public interface ICrud<T>
    {
        public bool insert(T entity);
        public List<T> findAll();
        public bool update(T entity);
        public bool delete(T entity);   
    }
}
