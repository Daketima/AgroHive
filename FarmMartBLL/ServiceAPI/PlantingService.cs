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
    public class PlantingService : IRepositoryService<Planting>
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IList<Planting> Get()
        {
            return unitOfWork.PlantingRepository.Get().ToList();
        }

        public Planting GetById(int? id)
        {
            return unitOfWork.PlantingRepository.GetByID(id);
        }

        public Planting Create(Planting Planting)
        {
            return unitOfWork.PlantingRepository.Insert(Planting);
        }

        public void Update(Planting Planting)
        {
            unitOfWork.PlantingRepository.Update(Planting);
        }

        public void Delete(Planting Planting)
        {
            unitOfWork.PlantingRepository.Delete(Planting);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
