using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GladeGeneratorGUI
{
    public partial class MainWindow : Window
    {
        private clsPartsParse clsPartsParse1 = null;
        private string saveDataFilePath = "./data.json";
        
        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
            
            if (!clsArgsConfig.Instance()._validateCommandKey())
            {
                return;
            }

            saveDataFilePath = clsFile._getExePath_replace(saveDataFilePath);

            _initConfigFile();
            
            try
            {
                clsPartsParse1 = new clsPartsParse(this);

                clsPartsParse1._parsePrjectFolder(clsArgsConfig.Instance().FileDirPath);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

            
        }

        private MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
        {
            builder.Autoconnect(this);

        }



    }
}