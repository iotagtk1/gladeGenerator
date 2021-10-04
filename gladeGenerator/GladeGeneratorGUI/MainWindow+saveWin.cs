using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GladeGeneratorGUI
{
    partial class MainWindow
    {
    
		private void on_openFolderBtn_clicked(object sender , EventArgs e)
		{

			fileDialog.Run();

		}
		
		private void on_closeBtn1_clicked(object sender , EventArgs e){

			this.Close();
			
		}
		
		
    }
}
