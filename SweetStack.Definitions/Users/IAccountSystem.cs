using SweetStack.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetStack.Definitions.Users
{
    public interface IAccountSystem
    {
        CreateAccountResult CreateAccount(SweetStack.DTO.Account user);
    }
}
