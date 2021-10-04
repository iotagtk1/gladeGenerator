using System;
using System.IO;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GladeGeneratorGUI
{
    partial class saveWin
    {
    
		private void on_openFolderBtn_clicked(object sender , EventArgs e)
		{

			string iniFolder = "";
			if (clsIniFile.singlton[saveWin.saveFileText1, saveWin.saveFilePath] != "")
			{
				iniFolder = clsIniFile.singlton[saveWin.saveFileText1, saveWin.saveFilePath];
			}else if (clsArgsConfig.Instance().FileDirPath != ""){
				iniFolder = clsFolder._getFolderNamePath(clsArgsConfig.Instance().FileDirPath);
			}

			fileDialog.SetCurrentFolder(iniFolder);
			fileDialog.Run();
		}
    
		private void on_closeBtn1_clicked(object sender , EventArgs e){
			Close();
		}
		
		
    }
}
