using FarmMartBLL.Core;
using FarmMartDAL.Implementation;
using FarmMartDAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmMartBLL.ServiceAPI
{
    public class LivestockTypeService : IRepositoryService<LivestockType>
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IList<LivestockType> Get()
        {
            return unitOfWork.LivestockTypeRepository.Get().ToList();
        }

        public LivestockType GetById(int? id)
        {
            return unitOfWork.LivestockTypeRepository.GetByID(id);
        }

        public LivestockType Create(LivestockType LivestockType)
        {
            return unitOfWork.LivestockTypeRepository.Insert(LivestockType);
        }

        public void Update(LivestockType LivestockType)
        {
            unitOfWork.LivestockTypeRepository.Update(LivestockType);
        }

        public void Delete(LivestockType LivestockType)
        {
            unitOfWork.LivestockTypeRepository.Delete(LivestockType);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
