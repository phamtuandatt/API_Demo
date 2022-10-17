using OA_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Repo
{
    public  interface IRepo<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(long id);
        T CheckLogin(Func<T, bool> entity);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void SaveChanges();
    }
}
