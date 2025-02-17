using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Entities;

namespace Test.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        // in this interface  you can add speceifc methods not included in generate repository of unit of work design pattern
    }
}
