using PalestraMVC.Models;
using PalestraMVC.Repository;

namespace PalestraMVC.Service
{
    public class CorsoService : IService<Corso>
    {
        private static IRepository<Corso> _repoCorso;

        public CorsoService(IRepository<Corso> repoCorso)
        {
            _repoCorso = repoCorso;
        }
        public List<Corso> findAll()
        {
            return(_repoCorso.findAll());
        }

        public Corso findByName(string stringa)
        {
            return (_repoCorso.findAll().SingleOrDefault(c => c.Codice == stringa));
        }

        public bool insert(Corso entity)
        {
            throw new NotImplementedException();
        }

        public bool update(Corso entity)
        {
            return(_repoCorso.update(entity));
        }
    }
}
