using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FrontendLayer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Open window
            Bootstrap();
        }

        /// <summary>
        /// Responsible class to activate the window
        /// </summary>
        public async void Bootstrap()
        {
            /// Don't show the blank window on start-up
            BrowserWindowOptions options = new BrowserWindowOptions { Show = false };

            /// Show window when ready
            BrowserWindow mainWindow = await Electron.WindowManager.CreateWindowAsync(options);
            mainWindow.OnReadyToShow += () =>
            {
                mainWindow.Show();
            };

            /// Create Menu
            MenuItem[] menu = new MenuItem[]
            {
                new MenuItem
                {
                    Label = "File",
                    Submenu = new MenuItem[]
                    {
                        new MenuItem
                        {
                            Label = "Exit",
                            Click = () =>
                            {
                                Electron.App.Exit();
                            }
                        }
                    }
                },
                new MenuItem
                {
                    Label = "Info",
                    Click = async () =>
                    {
                        await Electron.Dialog.ShowMessageBoxAsync("Welcome to James's Bitcoin Wallet.");
                    }
                }
            };

            /// Add menu to the App
            Electron.Menu.SetApplicationMenu(menu);
        }
    }
}
