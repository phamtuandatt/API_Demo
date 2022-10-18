using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OA_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Repo
{
    public class Repo<T> : IRepo<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _table;

        public Repo(ApplicationDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public T CheckLogin(Func<T, bool> entity)
        {
            return _table.FirstOrDefault(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public T Get(long id)
        {
            return _table.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _table.AsEnumerable();
        }

        public void Insert(T entity)
        {
            _table.Add(entity);
            _context.SaveChanges();
        }

        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _table.Update(entity);
            _context.SaveChanges();
        }
    }
}
