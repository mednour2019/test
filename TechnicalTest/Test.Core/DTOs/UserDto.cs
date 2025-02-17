using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Core.DTOs
{
    public class UserDto
    {
           public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required int Status { get; set; }
    }
}
