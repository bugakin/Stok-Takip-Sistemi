using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _3_StockControl_ServiceLayer.Services.Abstract
{
    public interface IGenericService<T>
    {
        bool Add(T entity);
        bool Add(List<T> items);
        bool Update(T entity);
        bool Remove(T entity);
        bool Remove(int id);

        bool RemoveAll(Expression<Func<T, bool>> expression);

        T GetById(int id);
        T GetByDefault(Expression<Func<T, bool>> expression);

        IQueryable<T> GetById(int id, params Expression<Func<T, object>>[] includes);
        List<T> GetActive();

        IQueryable<T> GetActive(params Expression<Func<T, object>>[] includes);
        List<T> GetDefault(Expression<Func<T, bool>> expression);
        List<T> GetAll();
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);

        IQueryable<T> GetAll(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        int Save();

        bool GetActive(int id);
        bool Any(Expression<Func<T, bool>> expression);
    }
}
