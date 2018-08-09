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
    public class MeasurementService : IRepositoryService<Measurement>
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IList<Measurement> Get()
        {
            return unitOfWork.MeasurementRepository.Get().ToList();
        }

        public Measurement GetById(int? id)
        {
            return unitOfWork.MeasurementRepository.GetByID(id);
        }

        public Measurement Create(Measurement Measurement)
        {
            return unitOfWork.MeasurementRepository.Insert(Measurement);
        }

        public void Update(Measurement Measurement)
        {
            unitOfWork.MeasurementRepository.Update(Measurement);
        }

        public void Delete(Measurement Measurement)
        {
            unitOfWork.MeasurementRepository.Delete(Measurement);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
