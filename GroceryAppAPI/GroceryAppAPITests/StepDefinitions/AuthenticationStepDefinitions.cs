using GroceryAppAPITests.Mocks;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryAppAPITests.StepDefinitions
{
    [Binding]
    [Scope(Feature = "Authentication")]
    public class AuthenticationStepDefinitions : BaseStepDefinitions
    {
        public AuthenticationStepDefinitions(CustomWebApplicationFactory applicationFactory)
            : base(applicationFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddTransient(_ => AuthenticationMock.UserRepositoryMock.Object);
                });
            }))
        {
        }
        [BeforeScenario]
        public void SetMocks() => AuthenticationMock.SetMocks();
    }
}
