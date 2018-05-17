using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FrontendLayer.Models;
using ElectronNET.API;

namespace FrontendLayer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Electron.IpcMain.On("async-msg", (args) =>
            {
                var mainWindow = Electron.WindowManager.BrowserWindows.First();

                /// Send (to which "Window", on which "channel", which "message")
                Electron.IpcMain.Send(mainWindow, "asynchronous-reply", "pong");
            });
            return View();
        }

        public IActionResult About()
        {
            //ViewData["Message"] = "Your application description page.";

            Electron.IpcMain.On("async-msg", (args) =>
            {
                var mainWindow = Electron.WindowManager.BrowserWindows.First();

                /// Send (to which "Window", on which "channel", which "message")
                Electron.IpcMain.Send(mainWindow, "asynchronous-reply", "pong");
            });
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
