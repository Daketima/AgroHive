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
    public class LivestockService : IRepositoryService<Livestock>
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IList<Livestock> Get()
        {
            return unitOfWork.LivestockRepository.Get().ToList();
        }

        public Livestock GetById(int? id)
        {
            return unitOfWork.LivestockRepository.GetByID(id);
        }

        public Livestock Create(Livestock Livestock)
        {
          return  unitOfWork.LivestockRepository.Insert(Livestock);
        }

        public void Update(Livestock Livestock)
        {
            unitOfWork.LivestockRepository.Update(Livestock);
        }

        public void Delete(Livestock Livestock)
        {
            unitOfWork.LivestockRepository.Delete(Livestock);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
