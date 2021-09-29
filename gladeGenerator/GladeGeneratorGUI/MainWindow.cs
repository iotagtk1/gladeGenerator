using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GladeGeneratorGUI
{
    class MainWindow : Window
    {

        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
            
            try
            {
                clsPartsParse clsPartsParse1 = new clsPartsParse();

                clsPartsParse1._parsePrjectFolder(clsArgsConfig.Instance().FileDirPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            
        }

        private MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
        {
            builder.Autoconnect(this);

        }



    }
}