using FarmMartBLL.Core;
using FarmMartDAL.Implementation;
using FarmMartDAL.Model;
using System.Collections.Generic;
using System.Linq;

namespace FarmMartBLL.ServiceAPI
{
    public class PersonService : IRepositoryService<Person>
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        
        public IList<Person> Get()
        {
            return unitOfWork.PersonRepository.Get().ToList() ;
        }

        public Person GetById(int? id)
        {
            return unitOfWork.PersonRepository.GetByID(id);
        }

        public Person Create(Person person)
        {
           return unitOfWork.PersonRepository.Insert(person);
        }

        public void Update(Person person)
        {
           unitOfWork.PersonRepository.Update(person);
        }

        public void Delete(Person person)
        {
            unitOfWork.PersonRepository.Delete(person);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
