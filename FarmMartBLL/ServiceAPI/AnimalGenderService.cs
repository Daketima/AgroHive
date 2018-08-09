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
    public class AnimalGenderService : IRepositoryService<AnimalGender>
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IList<AnimalGender> Get()
        {
            return unitOfWork.AnimalGenderRepository.Get().ToList();
        }

        public AnimalGender GetById(int? id)
        {
            return unitOfWork.AnimalGenderRepository.GetByID(id);
        }

        public AnimalGender Create(AnimalGender AnimalGender)
        {
            return unitOfWork.AnimalGenderRepository.Insert(AnimalGender);
        }

        public void Update(AnimalGender AnimalGender)
        {
            unitOfWork.AnimalGenderRepository.Update(AnimalGender);
        }

        public void Delete(AnimalGender AnimalGender)
        {
            unitOfWork.AnimalGenderRepository.Delete(AnimalGender);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
