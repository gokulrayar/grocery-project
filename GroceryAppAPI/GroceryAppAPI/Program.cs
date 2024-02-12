namespace GroceryAppAPI
{
    // Main entry point for the application
    public class Program
    {
        // Main method where the application starts execution
        public static void Main(string[] args)
        {
            // Build and run the host created by CreateHostBuilder method
            CreateHostBuilder(args).Build().Run();
        }

        // Creates a host builder for the application
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            // Create a default host builder and configure it for web hosting using ConfigureWebHostDefaults
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
        }
    }
}
