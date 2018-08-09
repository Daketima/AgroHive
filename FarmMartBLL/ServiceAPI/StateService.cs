using FarmMartBLL.Core;
using FarmMartDAL.Implementation;
using FarmMartDAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace FarmMartBLL.ServiceAPI
{
    public class StateService : IRepositoryService<State>
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public IList<State> Get()
        {
            return _unitOfWork.StateRepository.Get().ToList();
        }

        public State GetById(int? id)
        {
            return _unitOfWork.StateRepository.GetByID(id.Value);
        }

        public void Update(State State)
        {
            _unitOfWork.StateRepository.Update(State);

        }

        public void Delete(State State)
        {
            _unitOfWork.StateRepository.Delete(State);
            
        }

        public State Create(State State)
        {
           return _unitOfWork.StateRepository.Insert(State);
           
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        //public IQueryable<State> GetByQuery()
        //{
        //    return _unitOfWork.StateRepository.GetByQuery();
        //}
    }
}
