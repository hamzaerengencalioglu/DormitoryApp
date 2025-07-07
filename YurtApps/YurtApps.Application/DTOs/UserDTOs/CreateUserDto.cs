using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YurtApps.Application.Dtos.UserDtos
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Role {  get; set; }
    }
}
