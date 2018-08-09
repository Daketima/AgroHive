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
    public class FarmLivestockService : IRepositoryService<FarmLivestock>
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IList<FarmLivestock> Get()
        {
            return unitOfWork.FarmLivestockRepository.Get().ToList();
        }

        public FarmLivestock GetById(int? id)
        {
            return unitOfWork.FarmLivestockRepository.GetByID(id);
        }

        public FarmLivestock Create(FarmLivestock FarmLivestock)
        {
            return unitOfWork.FarmLivestockRepository.Insert(FarmLivestock);
        }

        public void Update(FarmLivestock FarmLivestock)
        {
            unitOfWork.FarmLivestockRepository.Update(FarmLivestock);
        }

        public void Delete(FarmLivestock FarmLivestock)
        {
            unitOfWork.FarmLivestockRepository.Delete(FarmLivestock);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
