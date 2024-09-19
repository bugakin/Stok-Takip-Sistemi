using _2_StockControl_DataAccessLayer.Context;
using _2_StockControl_DataAccessLayer.Repositories.Abstract;
using StockControl_EntityLayer.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Transactions;

namespace _2_StockControl_DataAccessLayer.Repositories.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Add(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                return Save() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Add(List<T> items)
        {
            try
            {
                using (TransactionScope ts=new())
                {
                    foreach (T item in items)
                    {
                        _context.Set<T>().Add(item);
                    }
                    ts.Complete();
                    return Save() > 0;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Any(Expression<Func<T, bool>> expression)=>_context.Set<T>().Any(expression);
        

        public List<T> GetActive()=>_context.Set<T>().Where(x=>x.IsActive==true).ToList();
       

        public IQueryable<T> GetActive(params Expression<Func<T, object>>[] includes)
        {
           var query=_context.Set<T>().Where(x=>x.IsActive==true);
            query=includes.Aggregate(query, (current,include)=>current.Include(include));
            return query;
            
        }

        public bool GetActive(int id)
        {
           var result=GetById(id);
            if (result is not null)
            {
                result.IsActive = true;
                
                return Update(result);
            }
            return false;

        }

        public List<T> GetAll()=>_context.Set<T>().ToList();
        

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var query=_context.Set<T>().AsQueryable();
            query=includes.Aggregate(query, (current,include)=>current.Include(include));
            return query;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            var query=_context.Set<T>().Where(expression);
            query = includes.Aggregate(query ,(current, include) => current.Include(include));
            return query;
        }

        public T GetByDefault(Expression<Func<T, bool>> expression)=> _context.Set<T>().FirstOrDefault(expression);


        public T GetById(int id) => _context.Set<T>().Find(id);
      
        public IQueryable<T> GetById(int id, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().Where(x=>x.ID==id);  
            query=includes.Aggregate(query,(current,include) => current.Include(include));
            return query;
            
        }

        public List<T> GetDefault(Expression<Func<T, bool>> expression)=>_context.Set<T>().Where(expression).ToList();
        

        public bool Remove(T entity)
        {
           entity.IsActive = false;
            return Update(entity);
        }

        public bool Remove(int id)
        {
            try
            {
                var result = GetById(id);
                return Remove(result);
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool RemoveAll(Expression<Func<T, bool>> expression)
        {
            try
            {
                using (TransactionScope ts = new())
                {
                    var result = GetDefault(expression);
                    int counter=0;
                    foreach (var item in result)
                    {
                       var operation=Remove(item);
                        if (operation) counter++;
                      
                    }
                    if(result.Count==counter) ts.Complete();
                    else return false;  
                }
                return true;
               
            }
            catch (Exception)
            {

                return false;
            }
        }

        public int Save()
        {
           return _context.SaveChanges();
        }

        public bool Update(T entity)
        {
            try
            {
                entity.ModifiedDate = DateTime.Now;
                _context.Set<T>().Update(entity);
                return Save() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
