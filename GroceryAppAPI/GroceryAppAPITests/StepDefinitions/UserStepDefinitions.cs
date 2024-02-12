using GroceryAppAPITests.Mocks;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryAppAPITests.StepDefinitions
{
    [Binding]
    [Scope(Feature ="User")]
    public class UserStepDefinitions : BaseStepDefinitions
    {
        public UserStepDefinitions(CustomWebApplicationFactory applicationFactory)
            : base(applicationFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddTransient(_ => UserMock.UserRepositoryMock.Object);
                });
            }))
        {
        }
        [BeforeScenario]
        public void SetMocks() => UserMock.SetMocks();
    }
}
