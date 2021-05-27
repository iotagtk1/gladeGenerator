using System;


namespace signalGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            
            clsGtkXmlParse.Instance();

            // please download 
            
            // https://github.com/mono/gtk-sharp/tree/main/doc
            
            // set absolute Path until Gtk and en
            
            clsGtkXmlParse.Instance().GtkFolderPath = "../../../doc/en/Gtk/";
            clsGtkXmlParse.Instance().GtkEnFolderPath = "../../../doc/en/";

            if (clsFolder._isFolder(clsGtkXmlParse.Instance().GtkFolderPath) && 
                clsFolder._isFolder(clsGtkXmlParse.Instance().GtkEnFolderPath)
            )
            {
                clsGtkXmlParse.Instance()._parse();
            }

        }
    }
}