using System;
using System.Collections.Generic;
using Gtk;

namespace GladeGeneratorGUI
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();
            
            try
            {
                MainWindow._initConfigFile();

                clsArgsConfig.Instance();

                clsArgsConfig.Instance()._setArgs(args);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var app = new Application("org.GladeGeneratorGUI.GladeGeneratorGUI", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            var win = new MainWindow();
            app.AddWindow(win);

            win.Show();
            Application.Run();
        }
    }
}