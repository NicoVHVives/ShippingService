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

namespace ShippingService.ServiceLayer.Services
{
    public class ShippingHistoryService : IService<ShippingHistory>
    {
        private readonly IRepository<ShippingHistory> _shippingHistoryRepository;
        private readonly IConfiguration _config;

        public ShippingHistoryService(IRepository<ShippingHistory> shippingHistoryRepository, IConfiguration config)
        {
            _shippingHistoryRepository = shippingHistoryRepository;
            _config = config;
        }

        public void Delete(ShippingHistory entity)
        {
            try
            {
                if(entity != null)
                {
                    _shippingHistoryRepository.Delete(entity);
                    _shippingHistoryRepository.SaveChanges();
                }
            }
            catch(Exception ex) {
                throw;
            }
        }

        public ShippingHistory Get(Expression<Func<ShippingHistory, bool>> whereClause)
        {
            try
            {
                var obj = _shippingHistoryRepository.Get(whereClause);
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

        public IEnumerable<ShippingHistory> GetAll()
        {
            try
            {
                var obj = _shippingHistoryRepository.GetAll();
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

        public IEnumerable<ShippingHistory> GetAll(Expression<Func<ShippingHistory, bool>> whereClause)
        {
            try
            {
                var obj = _shippingHistoryRepository.GetAll(whereClause);
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

        public void Insert(ShippingHistory entity)
        {
            try
            {
                if (entity != null)
                {
                    _shippingHistoryRepository.Insert(entity);
                    _shippingHistoryRepository.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Remove(ShippingHistory entity)
        {
            try
            {
                if (entity != null)
                {
                    _shippingHistoryRepository.Remove(entity);
                    _shippingHistoryRepository.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(ShippingHistory entity)
        {
            try
            {
                if (entity != null)
                {
                    _shippingHistoryRepository.Update(entity);
                    _shippingHistoryRepository.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetNrOfShipments(Guid clientGuid, DateTime dateTime)
        {
            try
            {
                return _shippingHistoryRepository.GetAll()
                    .Where(x => x.ClientGuid == clientGuid && x.CreatedDate > dateTime.AddDays(_config.GetValue<int>("ShippingParameters:FreeShipmentsPeriod") * -1)).Count();

                
                
                    
            }
            catch(Exception)
            {
                throw;
            }
        }

        public IEnumerable<ShippingHistory> GetAllForClientId(Guid clientGuid)
        {
            try
            {
                return GetAll()
                    .Where(x => x.ClientGuid == clientGuid).ToArray();

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
