using Microsoft.Extensions.Configuration;
using ShippingService.DomainLayer.Models;
using ShippingService.RepositoryLayer.IRepository;
using ShippingService.ServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShippingService.ServiceLayer.Services
{
    public class WebShopUserService : IIdentityService<WebShopUser>
    {
        private readonly IIdentityRepository<WebShopUser> _webShopUserRepository;
        private readonly IConfiguration _config;

        public WebShopUserService(IIdentityRepository<WebShopUser> webShopUserRepository, IConfiguration config)
        {
            _webShopUserRepository = webShopUserRepository;
            _config = config;
        }

        public void Delete(WebShopUser entity)
        {
            try
            {
                if(entity != null)
                {
                    _webShopUserRepository.Delete(entity);
                    _webShopUserRepository.SaveChanges();
                }
            }
            catch(Exception ex) {
                throw;
            }
        }

        public WebShopUser Get(string Id)
        {
            try
            {
                var obj = _webShopUserRepository.Get(Id);
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public WebShopUser Get(Expression<Func<WebShopUser, bool>> whereClause)
        {
            try
            {
                var obj = _webShopUserRepository.Get(whereClause);
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<WebShopUser> GetAll()
        {
            try
            {
                var obj = _webShopUserRepository.GetAll();
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Insert(WebShopUser entity)
        {
            try
            {
                if (entity != null)
                {
                    _webShopUserRepository.Insert(entity);
                    _webShopUserRepository.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Remove(WebShopUser entity)
        {
            try
            {
                if (entity != null)
                {
                    _webShopUserRepository.Remove(entity);
                    _webShopUserRepository.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(WebShopUser entity)
        {
            try
            {
                if (entity != null)
                {
                    _webShopUserRepository.Update(entity);
                    _webShopUserRepository.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

       
    }
}
