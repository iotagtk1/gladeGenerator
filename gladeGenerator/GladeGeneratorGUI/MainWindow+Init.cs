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

		    if (clsIniFile.singlton[dbKeyOrTableKey, "TreeViewEntry"] != "")
		    {
			    TreeViewEntry.Text = clsIniFile.singlton[dbKeyOrTableKey, "TreeViewEntry"];
		    }

		    if (clsIniFile.singlton[dbKeyOrTableKey, "ComboViewEntry"] != "")
		    {
			    ComboViewEntry.Text = clsIniFile.singlton[dbKeyOrTableKey, "ComboViewEntry"];
		    }

		    if (clsIniFile.singlton[dbKeyOrTableKey, "ListStoreEntry"] != "")
		    {
			    ListStoreEntry.Text = clsIniFile.singlton[dbKeyOrTableKey, "ListStoreEntry"];
		    }

		    if (clsIniFile.singlton[dbKeyOrTableKey, "ModelViewEntry"] != "")
		    {
			    ModelViewEntry.Text = clsIniFile.singlton[dbKeyOrTableKey, "ModelViewEntry"];
		    }
		    if (clsIniFile.singlton[dbKeyOrTableKey, "SubNameSpaceEntry"] != "")
		    {
			    SubNameSpaceEntry.Text = clsIniFile.singlton[dbKeyOrTableKey, "SubNameSpaceEntry"];
		    }

	    }
	  
		
    }
}
