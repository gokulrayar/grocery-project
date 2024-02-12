using GroceryAppAPITests.Mocks;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryAppAPITests.StepDefinitions
{
    [Binding]
    [Scope(Feature = "Order")]
    public class OrderStepDefinitions : BaseStepDefinitions
    {
        public OrderStepDefinitions(CustomWebApplicationFactory applicationFactory)
            : base(applicationFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddTransient(_ => OrderMock.OrderRepositoryMock.Object);
                    services.AddTransient(_ => OrderMock.UserRepositoryMock.Object);
                    services.AddTransient(_ => OrderMock.PaymentRepositoryMock.Object);
                    services.AddTransient(_ => OrderMock.ProductRepositoryMock.Object);
                    services.AddTransient(_ => OrderMock.OrderProductRepositoryMock.Object);
                });
            }))
        {
        }
        [BeforeScenario]
        public void SetMocks() => OrderMock.SetMocks();
    }
}
