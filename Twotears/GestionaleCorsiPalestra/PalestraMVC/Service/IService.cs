using PalestraMVC.Models;

namespace PalestraMVC.Service
{
    public interface IService<T>
    {
        public bool insert(T entity);
        public List<T> findAll();
        public T findByName(String stringa);
        public bool update(Corso entity);

    }
}
