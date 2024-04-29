using PalestraMVC.Models;
using PalestraMVC.Repository;

namespace PalestraMVC.Service
{
    public class UtenteService : IService<Utente>
    {
        private IRepository<Utente> _repository;   

        public UtenteService(IRepository<Utente> repository)
        {
            _repository = repository;
        }

        public List<Utente> findAll()
        {
            throw new NotImplementedException();
        }

        public Utente findByName(string stringa)
        {
            return (_repository.findAll().SingleOrDefault(u => u.Username == stringa));
        }

        public bool insert(Utente entity)
        {
            return(_repository.insert(entity));
        }

        public bool update(Corso entity)
        {
            throw new NotImplementedException();
        }
    }
}
