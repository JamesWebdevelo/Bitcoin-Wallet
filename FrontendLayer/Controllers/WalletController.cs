using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Models;
using ElectronNET.API;
using Microsoft.AspNetCore.Mvc;

namespace FrontendLayer.Controllers
{
    public class WalletController : Controller
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
    }
}