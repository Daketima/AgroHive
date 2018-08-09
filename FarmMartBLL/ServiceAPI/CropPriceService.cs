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
   public class CropPriceService : IRepositoryService<CropPrice>
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IList<CropPrice> Get()
        {
            return unitOfWork.CropPriceRepository.Get().ToList();
        }

        public CropPrice GetById(int? id)
        {
            return unitOfWork.CropPriceRepository.GetByID(id);
        }

        public CropPrice Create(CropPrice CropPrice)
        {
            return unitOfWork.CropPriceRepository.Insert(CropPrice);
        }

        public void Update(CropPrice CropPrice)
        {
            unitOfWork.CropPriceRepository.Update(CropPrice);
        }

        public void Delete(CropPrice CropPrice)
        {
            unitOfWork.CropPriceRepository.Delete(CropPrice);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
