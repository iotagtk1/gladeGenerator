please download doc folder

https://github.com/mono/gtk-sharp/tree/main/doc
            
set Program.cs absolute Path until Gtk and en

```
    clsGtkXmlParse.Instance().GtkFolderPath = "../../../doc/en/Gtk/";
    clsGtkXmlParse.Instance().GtkEnFolderPath = "../../../doc/en/";

    if (clsFolder._isFolder(clsGtkXmlParse.Instance().GtkFolderPath) && 
        clsFolder._isFolder(clsGtkXmlParse.Instance().GtkEnFolderPath)
    ) {
        clsGtkXmlParse.Instance()._parse();
    }
```
