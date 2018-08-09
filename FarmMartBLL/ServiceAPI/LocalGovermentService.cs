using FarmMartBLL.Core;
using FarmMartDAL.Implementation;
using FarmMartDAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace FarmMartBLL.ServiceAPI
{
    public class LocalGovermentService : IRepositoryService<LocalGovernment>
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public IList<LocalGovernment> Get()
        {
            return _unitOfWork.LocalGovernmentRepository.Get().ToList();
        }

        public LocalGovernment GetById(int? id)
        {
            return _unitOfWork.LocalGovernmentRepository.GetByID(id.Value);
        }

        public void Update(LocalGovernment LocalGovernment)
        {
            _unitOfWork.LocalGovernmentRepository.Update(LocalGovernment);
        }

        public void Delete(LocalGovernment LocalGovernment)
        {
            _unitOfWork.LocalGovernmentRepository.Delete(LocalGovernment);
        }

        public LocalGovernment Create(LocalGovernment LocalGovernment)
        {
             return _unitOfWork.LocalGovernmentRepository.Insert(LocalGovernment);
            
           
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

       

       

        //public IQueryable<LocalGovernment> GetByQuery()
        //{
        //    return _unitOfWork.LocalGovernmentRepository.GetByQuery();
        //}
    }
}
