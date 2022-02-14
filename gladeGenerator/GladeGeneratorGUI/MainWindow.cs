using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GladeGeneratorGUI
{
    public partial class MainWindow : Window
    {
        private clsPartsParse clsPartsParse1 = null;
        private string saveSignalDataFilePath = "./data.json";
        private string saveGladeTopPartDataFilePath = "./gladeTopPartData.json";

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

            saveSignalDataFilePath = clsFile._getExePath_replace(saveSignalDataFilePath);
            
            saveGladeTopPartDataFilePath = clsFile._getExePath_replace(saveGladeTopPartDataFilePath);

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
            DeleteEvent += Window_DeleteEvent;
        }
        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        public void _parsePrjectFolder()
        {
            clsPartsParse1._parsePrjectFolder(clsArgsConfig.Instance().FileDirPath);
        }


    }
}