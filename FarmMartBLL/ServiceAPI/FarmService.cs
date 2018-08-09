using FarmMartBLL.Core;
using FarmMartDAL.Implementation;
using FarmMartDAL.Model;
using System.Collections.Generic;
using System.Linq;

namespace FarmMartBLL.ServiceAPI
{
    public class FarmService : IRepositoryService<Farm>
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IList<Farm> Get()
        {
           return unitOfWork.FarmRepository.Get().ToList();
        }

        public Farm GetById(int? id)
        {
            return unitOfWork.FarmRepository.GetByID(id);
        }

        public Farm Create(Farm Farm)
        {
           return unitOfWork.FarmRepository.Insert(Farm);
        }

        public void Update(Farm Farm)
        {
            unitOfWork.FarmRepository.Update(Farm);
        }

        public void Delete(Farm Farm)
        {
            unitOfWork.FarmRepository.Delete(Farm);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
