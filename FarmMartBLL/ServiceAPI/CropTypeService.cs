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
   public class CropTypeService : IRepositoryService<CropType>
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IList<CropType> Get()
        {
            return unitOfWork.CropTypeRepository.Get().ToList();
        }

        public CropType GetById(int? id)
        {
            return unitOfWork.CropTypeRepository.GetByID(id);
        }

        public CropType Create(CropType CropType)
        {
            return unitOfWork.CropTypeRepository.Insert(CropType);
        }

        public void Update(CropType CropType)
        {
            unitOfWork.CropTypeRepository.Update(CropType);
        }

        public void Delete(CropType CropType)
        {
            unitOfWork.CropTypeRepository.Delete(CropType);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
