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
    public class PlantingPeriodService : IRepositoryService<HarvestPeriod>
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IList<HarvestPeriod> Get()
        {
            return unitOfWork.HarvestPeriodRepository.Get().ToList();
        }

        public HarvestPeriod GetById(int? id)
        {
            return unitOfWork.HarvestPeriodRepository.GetByID(id);
        }

        public HarvestPeriod Create(HarvestPeriod HarvestPeriod)
        {
            return unitOfWork.HarvestPeriodRepository.Insert(HarvestPeriod);
        }

        public void Update(HarvestPeriod HarvestPeriod)
        {
            unitOfWork.HarvestPeriodRepository.Update(HarvestPeriod);
        }

        public void Delete(HarvestPeriod HarvestPeriod)
        {
            unitOfWork.HarvestPeriodRepository.Delete(HarvestPeriod);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
