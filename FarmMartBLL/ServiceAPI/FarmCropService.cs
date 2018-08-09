using FarmMartBLL.Core;
using FarmMartDAL.Implementation;
using FarmMartDAL.Model;
using System.Collections.Generic;
using System.Linq;

namespace FarmMartBLL.ServiceAPI
{
    public class FarmCropService : IRepositoryService<FarmCrop>
    {

        private UnitOfWork unitOfWork = new UnitOfWork();

        public IList<FarmCrop> Get()
        {
            return unitOfWork.FarmCropRepository.Get().ToList();
        }

        public FarmCrop GetById(int? id)
        {
            return unitOfWork.FarmCropRepository.GetByID(id);
        }

        public FarmCrop Create(FarmCrop FarmCrop)
        {
          return  unitOfWork.FarmCropRepository.Insert(FarmCrop);
        }

        public void Update(FarmCrop FarmCrop)
        {
            unitOfWork.FarmCropRepository.Update(FarmCrop);
        }

        public void Delete(FarmCrop FarmCrop)
        {
            unitOfWork.FarmCropRepository.Delete(FarmCrop);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
