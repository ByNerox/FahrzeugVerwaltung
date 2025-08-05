using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FahrzeugVerwaltung
{
    public interface IRepository<TId, TModel>
    {
        void Save(TModel entity);
        TModel Get(TId ident);
        IEnumerable<TModel> GetAll();
        void Update(TModel entity);
        void Delete(TModel entity);
    }
}
