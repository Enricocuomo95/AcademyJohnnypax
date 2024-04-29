using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestitiLibri.DAL
{
    internal interface ElementCRUD<T>
    {
        public bool CreateInsert(T t);
        public List<T> ReadGetAll();
        public bool Update(T t);
        public bool Delete(T t);
    }
}
