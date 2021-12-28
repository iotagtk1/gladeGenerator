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
                
                List<string> a = new List<string>();
                a.Add("-projectName");
                a.Add("/home/ita/C#/samplecC#_core/GtkApplication8/GtkApplication8/MainWindow.glade");
                a.Add("-fileDir");
                a.Add("/home/ita/C#/samplecC#_core/GtkApplication8/GtkApplication8/MainWindow.glade");
                args = a.ToArray(); 
                

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