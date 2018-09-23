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
   public class CropVarietyService : IRepositoryService<CropVariety>
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IList<CropVariety> Get()
        {
            return unitOfWork.CropVarietyRepository.Get().ToList();
        }

        public CropVariety GetById(int? id)
        {
            return unitOfWork.CropVarietyRepository.GetByID(id);
        }

        public CropVariety Create(CropVariety CropVariety)
        {
            return unitOfWork.CropVarietyRepository.Insert(CropVariety);
        }

        public void Update(CropVariety CropVariety)
        {
            unitOfWork.CropVarietyRepository.Update(CropVariety);
        }

        public void Delete(CropVariety CropVariety)
        {
            unitOfWork.CropVarietyRepository.Delete(CropVariety);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
