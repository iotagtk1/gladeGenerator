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
	    private void _initConfigFile()
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

	    /*
	    private void _defaultTextFiledSet()
	    {
		    string ModelName = "";
	        string TreeViewName = "";
	        string ComboBoxName = "";
	        string ListStoreName = "";
	        string SubNameSpace = "";

	        switch (SelectedOutPutType)
	        {
	            case OutPutType.TreeView:
            		TreeViewTemplate TreeViewTemplate1 = new TreeViewTemplate();
            		ModelName = TreeViewTemplate1.ModelName;
            		TreeViewName = TreeViewTemplate1.TreeViewName;
            		ComboBoxName = TreeViewTemplate1.ComboBoxName;
            		ListStoreName = TreeViewTemplate1.ListStoreName;
            		SubNameSpace = TreeViewTemplate1.SubNameSpace;
            		break;
	            case OutPutType.TreeViewEx:
            		TreeViewTemplateEx TreeViewTemplateEx = new TreeViewTemplateEx();
            		ModelName = TreeViewTemplateEx.ModelName;
            		TreeViewName = TreeViewTemplateEx.TreeViewName;
            		ComboBoxName = TreeViewTemplateEx.ComboBoxName;
            		ListStoreName = TreeViewTemplateEx.ListStoreName;
            		SubNameSpace = TreeViewTemplateEx.SubNameSpace;
            		break;  
	            case OutPutType.ComboBox:
            		ComboBoxTemplate ComboBoxTemplate = new ComboBoxTemplate();
            		ModelName = ComboBoxTemplate.ModelName;
            		TreeViewName = ComboBoxTemplate.TreeViewName;
            		ComboBoxName = ComboBoxTemplate.ComboBoxName;
            		ListStoreName = ComboBoxTemplate.ListStoreName;
            		SubNameSpace = ComboBoxTemplate.SubNameSpace;
            		break;
	            case OutPutType.ComboBoxEx:
            		ComboBoxTemplateEx ComboBoxTemplateEx = new ComboBoxTemplateEx();
            		ModelName = ComboBoxTemplateEx.ModelName;
            		TreeViewName = ComboBoxTemplateEx.TreeViewName;
            		ComboBoxName = ComboBoxTemplateEx.ComboBoxName;
            		ListStoreName = ComboBoxTemplateEx.ListStoreName;
            		SubNameSpace = ComboBoxTemplateEx.SubNameSpace;
            		break;
	        }

	        TreeViewEntry.Text = TreeViewName;
	        ComboViewEntry.Text = ComboBoxName;
	        ListStoreEntry.Text = ListStoreName;
	        ModelViewEntry.Text = ModelName;
	        SubNameSpaceEntry.Text = SubNameSpace;

	    }
	    */
	    
		
    }
}
