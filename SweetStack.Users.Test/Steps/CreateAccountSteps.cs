using NUnit.Framework;
using SweetStack.DTO;
using SweetStack.Users.Providers;
using System;
using TechTalk.SpecFlow;

namespace SweetStack.Users.Test.Steps
{
    [Binding]
    public class CreateAccountSteps
    {
        [Given]
        public void GivenIHaveAnAccountService()
        {
            var provider = Rhino.Mocks.MockRepository.GenerateStub<IAccountProvider>();
            ScenarioContext.Current.Add("AccountService", new AccountSystem(provider));
        }

        [When]
        public void WhenICreateAnAccount()
        {
            var accSys = ScenarioContext.Current.Get<AccountSystem>("AccountService");
            var user = new Account{
                Name="Kaylee"
            };
            ScenarioContext.Current.Add("CreateAccountResult", accSys.CreateAccount(user));
        }

        [Then]
        public void ThenTheAccountShouldBeCreated()
        {
            var createResult = ScenarioContext.Current.Get<CreateAccountResult>("CreateAccountResult");
            Assert.That(createResult.Status, Is.EqualTo(AccountStatus.Created));
        }

        [When]
        public void WhenICreateAnAccountWithAMissingName()
        {
            Action a = () =>
            {

                var accSys = ScenarioContext.Current.Get<AccountSystem>("AccountService");
                var user = new Account();
                ScenarioContext.Current.Add("CreateAccountResult", accSys.CreateAccount(user));
            };

            ScenarioContext.Current.Add("CreateAccountFunction", a);
        }

        [Then]
        public void ThenTheAccountShouldNotBeCreated()
        {
            Assert.Throws<Exception>(() => {
                var createOperation = ScenarioContext.Current.Get<Action>("CreateAccountResult");
                createOperation();
            });
        }


    }
}
