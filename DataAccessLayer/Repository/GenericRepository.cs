using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class GenericRepository<T> : IGeneericDal<T> where T : class
    {
        public void Delete(T t)
        {
            try
            {
                var c = new DataContext();
                c.Set<T>().Attach(t);
                c.Set<T>().Remove(t);
                c.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

            

        }

        public List<T> GetAllList()
        {
            var c = new DataContext();
            return c.Set<T>().ToList();
        }

        public List<T> GetAllList(Expression<Func<T, bool>> filter)
        {
            var c = new DataContext();
            return c.Set<T>().Where(filter).ToList();
        }

        public T GetById(int id)
        {
            var c = new DataContext();
            return c.Set<T>().Find(id);
        }

        public void Insert(T t)
        {
            DataContext myContext = new DataContext();
            myContext.Set<T>().Add(t);
            myContext.SaveChanges();
        }

        public void Update(T t)
        {
            DataContext myContext = new DataContext();
            myContext.Set<T>().AddOrUpdate(t);
            myContext.SaveChanges();
        }
    }
}
