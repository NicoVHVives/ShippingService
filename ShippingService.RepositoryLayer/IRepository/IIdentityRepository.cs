using ShippingService.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShippingService.RepositoryLayer.IRepository
{
    public interface IIdentityRepository <T> where T : WebShopUser
    {
        IEnumerable<T> GetAll ();
        T Get(string Id);

        T Get(Expression<Func<T,bool>> whereClause);
        T GetByName(string name);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void SaveChanges();
    }
    
    
}
