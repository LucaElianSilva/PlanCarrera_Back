using Microsoft.AspNetCore;

namespace PlanCarrera.WebApi {
    public static class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        //Este metodo buildea el servidor web y corre el startup con todas las configuraciones necesarias para el proyecto.
        public static IWebHost BuildWebHost(string[] args)
        {
            var webHostBuilder = WebHost.CreateDefaultBuilder(args);
            return webHostBuilder.UseStartup<Startup>()
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddConsole();
                        logging.SetMinimumLevel(LogLevel.Trace);
                    })
                    .Build();
        }

    }
}
