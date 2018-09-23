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
    public class CropService : IRepositoryService<Crop>
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IList<Crop> Get()
        {
            return unitOfWork.CropRepository.Get().ToList();
        }

        public Crop GetById(int? id)
        {
            return unitOfWork.CropRepository.GetByID(id);
        }

        public Crop Create(Crop Crop)
        {
          return  unitOfWork.CropRepository.Insert(Crop);
        }

        public void Update(Crop Crop)
        {
            unitOfWork.CropRepository.Update(Crop);
        }

        public void Delete(Crop Crop)
        {
            unitOfWork.CropRepository.Delete(Crop);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
