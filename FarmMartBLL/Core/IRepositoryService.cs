using FarmMartDAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmMartBLL.Core
{
    public interface IRepositoryService<T>
    {
        IList<T> Get();
        T GetById(int? id);
        void Update(T Crop);
        void Delete(T Crop);
        T Create(T Crop);
    }
}
