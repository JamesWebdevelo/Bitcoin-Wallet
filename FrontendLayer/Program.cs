using BusinessLayer;
using ElectronNET.API;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace FrontendLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
            /// Run the Configuration on the Business Layer
            Config.Load();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseElectron(args) // Builder extension
                .Build();
    }
}
