global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace FrontEnd {
    internal static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain());
        }
    }
}