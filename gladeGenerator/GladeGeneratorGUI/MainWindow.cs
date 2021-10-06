using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GladeGeneratorGUI
{
    public partial class MainWindow : Window
    {
        private clsPartsParse clsPartsParse1 = null;
        private string saveDataFilePath = "./data.json";

        static public string ListStoreEntryStr = "ListStoreEntry";
        static public string ModelViewEntryStr = "ModelViewEntry";

        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
            
            clsPartsParse1 = new clsPartsParse(this);
            
            _mkTreeView_TopLevelPart();
            _mkTreeView_ChildLevelPart();
            _mkTreeView_Signal();
            
            if (!clsArgsConfig.Instance()._validateCommandKey())
            {
                return;
            }

            saveDataFilePath = clsFile._getExePath_replace(saveDataFilePath);

            try
            {
                
                _parsePrjectFolder();
                
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

        public void _parsePrjectFolder()
        {
            clsPartsParse1._parsePrjectFolder(clsArgsConfig.Instance().FileDirPath);
        }


    }
}