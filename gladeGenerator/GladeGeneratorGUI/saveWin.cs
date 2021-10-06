using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GladeGeneratorGUI
{
    public partial class saveWin : Window
    {

        static  public string saveFilePath = "saveFilePath";
        static  public string saveFileText1 = "saveFileText1";
        private Builder builder1 = null;
        private MainWindow _mainWindow;

        public saveWin(MainWindow mainWindow) : this(new Builder("saveWin.glade"))
        {
            _mainWindow = mainWindow;
            
            if (clsIniFile.singlton[saveWin.saveFileText1, saveWin.saveFilePath] != "")
            {
                saveFileText.Text = clsIniFile.singlton[saveWin.saveFileText1, saveWin.saveFilePath];
            }

        }

        private saveWin(Builder builder) : base(builder.GetRawOwnedObject("saveWin"))
        {
            builder.Autoconnect(this);

            builder1 = builder;

        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

    }
}