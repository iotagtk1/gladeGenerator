using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GladeGeneratorGUI
{
    public partial class saveWin : Window
    {

        static  public string saveFilePath = "saveFilePath";
        static  public string saveFileText1 = clsArgsConfig.Instance().ProjectName._md5();
        private Builder builder1 = null;
        private MainWindow _mainWindow;

        public saveWin(MainWindow mainWindow) : this(new Builder("saveWin.glade"))
        {
            _mainWindow = mainWindow;

            saveWin.saveFileText1 = clsArgsConfig.Instance().ProjectName._md5();
            
            if (clsIniFile.singlton[saveWin.saveFileText1, saveWin.saveFilePath] != "")
            {
                saveFileText.Text = clsIniFile.singlton[saveWin.saveFileText1, saveWin.saveFilePath];
            }
            
        }

        private saveWin(Builder builder) : base(builder.GetRawOwnedObject("saveWin"))
        {
            builder.Autoconnect(this);

            builder1 = builder;

            //DeleteEvent += Window_DeleteEvent;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            //Application.Quit();
        }

    }
}