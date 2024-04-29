using PalestraMVC.Models;

namespace PalestraMVC.Repository
{
    public interface IRepository<T>
    {
        public bool insert(T entity);
        public List<T> findAll();
        public bool update(T entity);

    }
}
