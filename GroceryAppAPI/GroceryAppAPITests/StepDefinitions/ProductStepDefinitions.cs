using GroceryAppAPITests.Mocks;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryAppAPITests.StepDefinitions
{
    [Binding]
    [Scope(Feature = "Product")]
    public class ProductStepDefinitions : BaseStepDefinitions
    {
        public ProductStepDefinitions(CustomWebApplicationFactory applicationFactory)
            : base(applicationFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddTransient(_ => ProductMock.ProductRepositoryMock.Object);
                });
            }))
        {
        }
        [BeforeScenario]
        public void SetMocks() => ProductMock.SetMocks();
    }
}
