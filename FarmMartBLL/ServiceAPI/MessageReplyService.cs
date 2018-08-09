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
    public class MessageReplyService : IRepositoryService<MessageReply>
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IList<MessageReply> Get()
        {
            return unitOfWork.MessageReplyRepository.Get().ToList();
        }

        public MessageReply GetById(int? id)
        {
            return unitOfWork.MessageReplyRepository.GetByID(id);
        }

        public MessageReply Create(MessageReply MessageReply)
        {
            return unitOfWork.MessageReplyRepository.Insert(MessageReply);
        }

        public void Update(MessageReply MessageReply)
        {
            unitOfWork.MessageReplyRepository.Update(MessageReply);
        }

        public void Delete(MessageReply MessageReply)
        {
            unitOfWork.MessageReplyRepository.Delete(MessageReply);
        }
    }
}
