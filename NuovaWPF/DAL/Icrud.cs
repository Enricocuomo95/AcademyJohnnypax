using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abbigliamento.DAL
{
    internal interface Icrud <T>
    {
        public bool CreateInsertValue(T entity);
        public List<T> ReadGetValue();
        public bool UpdateValue (T entity);
        public bool DeleteValue (T entity);

    }
}
