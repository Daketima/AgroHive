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
   public class LivestockBreedService : IRepositoryService<LivestockBreed>
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IList<LivestockBreed> Get()
        {
            return unitOfWork.LivestockBreedRepository.Get().ToList();
        }

        public LivestockBreed GetById(int? id)
        {
            return unitOfWork.LivestockBreedRepository.GetByID(id);
        }

        public LivestockBreed Create(LivestockBreed LivestockBreed)
        {
            return unitOfWork.LivestockBreedRepository.Insert(LivestockBreed);
        }

        public void Update(LivestockBreed LivestockBreed)
        {
            unitOfWork.LivestockBreedRepository.Update(LivestockBreed);
        }

        public void Delete(LivestockBreed LivestockBreed)
        {
            unitOfWork.LivestockBreedRepository.Delete(LivestockBreed);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
