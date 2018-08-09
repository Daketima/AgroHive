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
    public class CropService : IRepositoryService<CropVariety>
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IList<CropVariety> Get()
        {
            return unitOfWork.CropRepository.Get().ToList();
        }

        public CropVariety GetById(int? id)
        {
            return unitOfWork.CropRepository.GetByID(id);
        }

        public CropVariety Create(CropVariety Crop)
        {
          return  unitOfWork.CropRepository.Insert(Crop);
        }

        public void Update(CropVariety Crop)
        {
            unitOfWork.CropRepository.Update(Crop);
        }

        public void Delete(CropVariety Crop)
        {
            unitOfWork.CropRepository.Delete(Crop);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
