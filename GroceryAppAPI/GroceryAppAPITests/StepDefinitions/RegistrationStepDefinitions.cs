using GroceryAppAPITests.Mocks;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryAppAPITests.StepDefinitions
{
    [Binding]
    [Scope(Feature = "Registration")]
    public class RegistrationStepDefinitions : BaseStepDefinitions
    {
        public RegistrationStepDefinitions(CustomWebApplicationFactory applicationFactory)
            : base(applicationFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddTransient(_ => RegistrationMock.UserRepositoryMock.Object);
                });
            }))
        {
        }
        [BeforeScenario]
        public void SetMocks() => RegistrationMock.SetMocks();
    }
}
