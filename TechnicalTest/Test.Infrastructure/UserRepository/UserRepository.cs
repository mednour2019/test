using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Entities;
using Test.Core.Interfaces;
using Test.Infrastructure.Data;
using Test.Infrastructure.GenericRepository;

namespace Test.Infrastructure.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContextTechnicalTest context) : base(context) { }
    }
}
