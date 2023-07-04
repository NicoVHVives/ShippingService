using ShippingService.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShippingService.RepositoryLayer.IRepository
{
    public interface IRepository <T> where T : BaseEntity
    {
        IEnumerable<T> GetAll ();

        IEnumerable<T> GetAll(Expression<Func<T, bool>> whereClause);
        T Get(Expression<Func<T, bool>> whereClause);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void SaveChanges();
    }
    
    
}
