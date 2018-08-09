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
    public class LivestockPriceService : IRepositoryService<LivestockPrice>
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IList<LivestockPrice> Get()
        {
            return unitOfWork.LivestockPriceRepository.Get().ToList();
        }

        public LivestockPrice GetById(int? id)
        {
            return unitOfWork.LivestockPriceRepository.GetByID(id);
        }

        public LivestockPrice Create(LivestockPrice LivestockPrice)
        {
            return unitOfWork.LivestockPriceRepository.Insert(LivestockPrice);
        }

        public void Update(LivestockPrice LivestockPrice)
        {
            unitOfWork.LivestockPriceRepository.Update(LivestockPrice);
        }

        public void Delete(LivestockPrice LivestockPrice)
        {
            unitOfWork.LivestockPriceRepository.Delete(LivestockPrice);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
