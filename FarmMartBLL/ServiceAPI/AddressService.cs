using FarmMartBLL.Core;
using System.Collections.Generic;
using System.Linq;
using FarmMartDAL.Implementation;
using FarmMartDAL.Model;

namespace FarmMartBLL.ServiceAPI
{
    public class AddressService : IRepositoryService<Address>
    {

        private UnitOfWork unitOfWork = new UnitOfWork();

        public IList<Address> Get()
        {
            return unitOfWork.AddressRepository.Get().ToList();
        }

        public Address GetById(int? id)
        {
            return unitOfWork.AddressRepository.GetByID(id);
        }

        public Address Create(Address Address)
        {
           return  unitOfWork.AddressRepository.Insert(Address);
        }

        public void Update(Address Address)
        {
            unitOfWork.AddressRepository.Update(Address);
        }

        public void Delete(Address Address)
        {
            unitOfWork.AddressRepository.Delete(Address);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }  
}
