using GroceryAppAPITests.Mocks;
using Microsoft.Extensions.DependencyInjection;
namespace GroceryAppAPITests.StepDefinitions
{
    [Binding]
    [Scope(Feature = "Cart")]
    public class CartStepDefinitions : BaseStepDefinitions
    {
        public CartStepDefinitions(CustomWebApplicationFactory applicationFactory)
            : base(applicationFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddTransient(_ => CartMock.CartRepositoryMock.Object);
                    services.AddTransient(_ => CartMock.CartProductRepositoryMock.Object);
                    services.AddTransient(_ => CartMock.ProductRepositoryMock.Object);
                    services.AddTransient(_ => CartMock.UserRepositoryMock.Object);
                });
            }))
        {
        }
        [BeforeScenario]
        public void SetMocks() => CartMock.SetMocks();
    }
}
