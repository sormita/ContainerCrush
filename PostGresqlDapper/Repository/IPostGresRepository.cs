using PostGresqlDapper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostGresqlDapper.Repository
{
    public interface IPostGresRepository<T> where T:BaseModel
    {
        //T GetById(int id);
        List<T> GetAll();
        //ICollection<T> GetAll(string where);
        //void Update(T entity);
        //void Insert(T entity);
        //bool Delete(T entity);
        //bool Delete(ICollection<T> entityes);
    }
}
