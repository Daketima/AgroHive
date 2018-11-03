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
    public class FarmSizeUnitService : IRepositoryService<FarmSizeUnit>
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IList<FarmSizeUnit> Get()
        {
            return unitOfWork.FarmSizeUnitRepository.Get().ToList();
        }

        public FarmSizeUnit GetById(int? id)
        {
            return unitOfWork.FarmSizeUnitRepository.GetByID(id);
        }

        public FarmSizeUnit Create(FarmSizeUnit Farm)
        {
            return unitOfWork.FarmSizeUnitRepository.Insert(Farm);
        }

        public void Update(FarmSizeUnit Farm)
        {
            unitOfWork.FarmSizeUnitRepository.Update(Farm);
        }

        public void Delete(FarmSizeUnit Farm)
        {
            unitOfWork.FarmSizeUnitRepository.Delete(Farm);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
