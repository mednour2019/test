using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
       
            IGenericRepository<T> GetRepository<T>() where T : class;
            Task<int> SaveAsync();
        

    }
}
