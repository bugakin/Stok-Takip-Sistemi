using _2_StockControl_DataAccessLayer.Repositories.Abstract;
using _3_StockControl_ServiceLayer.Services.Abstract;
using StockControl_EntityLayer.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _3_StockControl_ServiceLayer.Services.Concrete
{
    public class GenericManager<T> : IGenericService<T> where T : BaseEntity
    {
        private readonly IGenericRepository<T> _repo;

        public GenericManager(IGenericRepository<T> repository)
        {
            _repo = repository;
        }

        public bool Add(T entity)
        {
            if (entity is null)
            {
                return false;
            }
            else return _repo.Add(entity);
        }

        public bool Add(List<T> items)
        {
           if(items is null)return false;
           else return _repo.Add(items);
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return _repo.Any(expression);
        }

        public List<T> GetActive()
        {
            return _repo.GetActive();   
        }

        public IQueryable<T> GetActive(params Expression<Func<T, object>>[] includes)
        {
            return _repo.GetActive(includes);
        }

        public bool GetActive(int id)
        {
            if (id == 0 || GetById(id) == null) return false;
            else return _repo.GetActive(id);
        }

        public List<T> GetAll()
        {
            return _repo.GetAll();
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            return _repo.GetAll(includes);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            return _repo.GetAll(expression, includes);  
        }

        public T GetByDefault(Expression<Func<T, bool>> expression)
        {
           return _repo.GetByDefault(expression);
        }

        public T GetById(int id)
        {
            if (id == 0) return null;
            else return _repo.GetById(id);
        }

        public IQueryable<T> GetById(int id, params Expression<Func<T, object>>[] includes)
        {
            if (id == 0) return null;
            else return _repo.GetById(id, includes);
        }

        public List<T> GetDefault(Expression<Func<T, bool>> expression)
        {
           return _repo.GetDefault(expression); 
        }

        public bool Remove(T entity)
        {
            if(entity == null) return false;    
            else return _repo.Remove(entity);   
        }

        public bool Remove(int id)
        {
            if(id == 0) return false;
            else return _repo.Remove(id);
        }

        public bool RemoveAll(Expression<Func<T, bool>> expression)
        {
            return _repo.RemoveAll(expression);
        }

        public int Save()
        {
            return _repo.Save();
        }

        public bool Update(T entity)
        {
            if(entity is null)return false;
            else return _repo.Update(entity);
        }
    }
}
