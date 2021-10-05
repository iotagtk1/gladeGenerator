using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Gtk;

using UI = Gtk.Builder.ObjectAttribute;

namespace GladeGeneratorGUI
{
    partial class MainWindow
    {
	    static public void _initConfigFile()
	    {

		    string configIniPath = clsFile._getExePath_replace("./config.ini");
		    
		    clsIniFile._sharedObject(configIniPath);

	    }

	    private void _initTextFiled(string dbKeyOrTableKey){
	
		    if (clsIniFile.singlton[dbKeyOrTableKey, MainWindow.ListStoreEntryStr] != "")
		    {
			    ListStoreEntry.Text = clsIniFile.singlton[dbKeyOrTableKey, MainWindow.ListStoreEntryStr];
		    }
		    else
		    {
			    ListStoreEntry.Text = "ListStore";
		    }
		    
		    if (clsIniFile.singlton[dbKeyOrTableKey, MainWindow.ModelViewEntryStr] != "")
		    {
			    ModelViewEntry.Text = clsIniFile.singlton[dbKeyOrTableKey, MainWindow.ModelViewEntryStr];
		    } else
		    {
			    ListStoreEntry.Text = "ModelName";
		    }

	    }
	  
		
    }
}
