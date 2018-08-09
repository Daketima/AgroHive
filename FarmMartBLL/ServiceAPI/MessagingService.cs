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
    public class MessagingService : IRepositoryService<Messaging>
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IList<Messaging> Get()
        {
            return unitOfWork.MessagingRepository.Get().ToList();
        }

        public Messaging GetById(int? id)
        {
            return unitOfWork.MessagingRepository.GetByID(id);
        }

        public Messaging Create(Messaging Messaging)
        {
            return unitOfWork.MessagingRepository.Insert(Messaging);
        }

        public void Update(Messaging Messaging)
        {
            unitOfWork.MessagingRepository.Update(Messaging);
        }

        public void Delete(Messaging Messaging)
        {
            unitOfWork.MessagingRepository.Delete(Messaging);
        }
    }
}
