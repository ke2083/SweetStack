using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweetStack.Definitions.Users.Providers
{
    public interface IAccountProvider
    {
        void Add(SweetStack.DTO.Account user);
        
    }
}
