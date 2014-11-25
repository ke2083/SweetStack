namespace SweetStack.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SweetStack.DTO;
    using SweetStack.Definitions.Users;
    using SweetStack.Definitions.Users.Providers;
    using SweetStack.Definitions.Users.Validators;

    public class AccountSystem : IAccountSystem
    {
        private IAccountProvider _provider = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="AccountSystem"/> class.
        /// </summary>
        public AccountSystem(IAccountProvider provider, IUserValidator userValidator)
        {
            _provider = provider;
        }

        public CreateAccountResult CreateAccount(Account user)
        {
            _provider.Add(user);
            return new CreateAccountResult
            {
                Status = AccountStatus.Created
            };
        }
    }
}
