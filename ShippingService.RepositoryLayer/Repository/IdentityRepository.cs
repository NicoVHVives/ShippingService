using Microsoft.EntityFrameworkCore;
using ShippingService.DomainLayer.Models;
using ShippingService.RepositoryLayer.Data;
using ShippingService.RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShippingService.RepositoryLayer.Repository
{
    public class IdentityRepository<T> : IIdentityRepository<T> where T : WebShopUser
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private DbSet<T> entities;

        public IdentityRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            entities = _applicationDbContext.Set<T>();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            _applicationDbContext.SaveChanges();
        }
        
        public T Get(string Id)
        {
            return entities.SingleOrDefault(c => c.Id == Id.ToString());
        }


        public T Get(Expression<Func<T, bool>> whereClause)
        {
            return entities.SingleOrDefault(whereClause);
        }

        public T GetByName(string name)
        {
            return entities.SingleOrDefault(c => c.UserName == name);
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            _applicationDbContext.SaveChanges();
        }
        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }
        public void SaveChanges()
        {
            _applicationDbContext.SaveChanges();
        }
        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            _applicationDbContext.SaveChanges();
        }

      
    }
}
