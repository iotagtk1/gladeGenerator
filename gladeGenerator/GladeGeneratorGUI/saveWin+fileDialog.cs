using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GladeGeneratorGUI
{
    partial class saveWin
    {
	    private void on_okBtn_clicked(object sender , EventArgs e)
	    {

		    if (clsFolder._isFolder(fileDialog.Filename))
		    {
			    saveFileText.Text = fileDialog.Filename;
			
			    clsIniFile.singlton[saveWin.saveFileText1,  saveWin.saveFilePath] = fileDialog.Filename;

			    clsArgsConfig.Instance().SaveDir = fileDialog.Filename; 
		    }
		    else
		    {
			    Console.WriteLine("Folderがない");
		    }
		    
		    fileDialog.Destroy();
	    }
		private void on_cancelBtn_clicked(object sender , EventArgs e)
		{

			fileDialog.Destroy();

		}
	
		
    }
}
