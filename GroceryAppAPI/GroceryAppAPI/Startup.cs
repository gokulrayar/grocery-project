using GroceryAppAPI.Configurations;
using GroceryAppAPI.Repository.Interfaces;
using GroceryAppAPI.Repository;
using GroceryAppAPI.Services.Interfaces;
using GroceryAppAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GroceryAppAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        // Constructor to initialize configuration
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Configuration of services for dependency injection
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsConfiguration = Configuration.GetSection("AppSettings");
            var connectionStringConfiguration = appSettingsConfiguration.GetSection("ConnectionString");

            // Configure ConnectionString options from app settings
            services.Configure<ConnectionString>(connectionStringConfiguration);

            // Registering repositories
            services.AddTransient<ICartProductRepository, CartProductRepository>();
            services.AddTransient<IOrderProductRepository, OrderProductRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            // Registering services
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRegistrationService, RegistrationService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IOrderService, OrderService>();

            // Adding HttpContextAccessor as a singleton
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Configuring JWT authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["AppSettings:Authentication:Issuer"],
                    ValidAudience = Configuration["AppSettings:Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:Authentication:Key"]))
                };
            });

            // Adding controllers and Swagger documentation
            services.AddControllers();
            services.AddSwaggerGen();
        }

        // Configuration of the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable development-specific features if in the development environment
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Redirect HTTP requests to HTTPS
            app.UseHttpsRedirection();

            // Enable authentication, routing, authorization, and endpoint mapping
            app.UseAuthentication();
            app.UseRouting();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
