using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GladeGeneratorGUI
{
    partial class MainWindow
    {

        private GladeData SelectedGladeDataRow = null; 
        private TopLevelPart SelectedTopLevelPartRow = null;
        private ChildLevelPart SelectedChildLevelPartRow = null;
        private Signal SelectedSignalRow = null;
        private string SelectedTopChildKey = "";

        private void on_TopLevelPartTreeViewSection_changed(object sender, EventArgs e)
        {
            TreeIter iter;
            ITreeModel model;
            if (((Gtk.TreeSelection)sender).GetSelected(out model, out iter)){
                TopLevelPart TopLevelPart1 = (TopLevelPart)TopLevelPartListStore.GetValue(iter, 0);
                SelectedTopLevelPartRow = TopLevelPart1;

                SelectedTopChildKey = _getTopLevelPartKey();

                _initTextFiled(SelectedTopChildKey);

                _mkTreeView_ChildLevelPart(TopLevelPart1.ChildLevelPartsArray);

                _mkTreeView_Signal(TopLevelPart1.SignalArray,SelectedTopChildKey);

                _saveAll(SelectedTopChildKey);

            }
        }

        private void on_ChildLevelPartTreeViewSection_changed(object sender, EventArgs e)
        {
            TreeIter iter;
            ITreeModel model;
            if (((Gtk.TreeSelection)sender).GetSelected(out model, out iter))
            {
                ChildLevelPart ChildLevelPart1 = (ChildLevelPart)ChildLevelPartListStore.GetValue(iter, 0);
                SelectedChildLevelPartRow = ChildLevelPart1;
                
                SelectedTopChildKey = _getTopLevelPartChildPartKey();
                
                _initTextFiled(SelectedTopChildKey);
                
                _mkTreeView_Signal(ChildLevelPart1.SignalArray,SelectedTopChildKey);

                _saveAll(SelectedTopChildKey);

            }
        }

        private void on_SignalTreeViewSection_changed(object sender, EventArgs e)
        {
            TreeIter iter;
            ITreeModel model;
            if (((Gtk.TreeSelection)sender).GetSelected(out model, out iter))
            {
                Signal Signal1 = (Signal)SignalListStore.GetValue(iter, 0);
                SelectedSignalRow = Signal1;
                Signal1.isReOutPut = Signal1.isReOutPut == true ? false : true;
                _saveAll(SelectedTopChildKey);
            }
        }


        private void on_TreeViewEntry_changed(object sender , EventArgs e){
            
            string text = ((Gtk.Entry)sender).Text;
            if (SelectedTopChildKey != "")
            {
                clsIniFile.singlton[SelectedTopChildKey,"TreeViewEntry"] = text;
            }

        }

        private void on_ListStoreEntry_changed(object sender , EventArgs e){

            string text = ((Gtk.Entry)sender).Text;
            if (SelectedTopChildKey != "")
            {
                clsIniFile.singlton[SelectedTopChildKey,"TreeViewEntry"] = text;
            }

        }
        
        private void on_ModelViewEntry_changed(object sender , EventArgs e){
            
            string text = ((Gtk.Entry)sender).Text;
            if (SelectedTopChildKey != "")
            {
                clsIniFile.singlton[SelectedTopChildKey,"TreeViewEntry"] = text;
            }
            
        }
        private void on_SubNameSpaceEntry_changed(object sender , EventArgs e){
            
            string text = ((Gtk.Entry)sender).Text;
            if (SelectedTopChildKey != "")
            {
                clsIniFile.singlton[SelectedTopChildKey,"TreeViewEntry"] = text;
            }

        }

        private void on_closeBtn_clicked(object sender, EventArgs e)
        {
            Close();
            clsUtility._appEnd();
        }
        
        private void on_outPutBtn_clicked(object sender, EventArgs e)
        {
            clsPartsParse1._outPut();
        }
    
		private void on_defualtSetBtn_clicked(object sender , EventArgs e){
			/*ListStore_MainWindow.Foreach (delegate (ITreeModel model, TreePath path, TreeIter iter)  {
			
			    type_MainWindow modelObj = model.GetValue(iter, 0) as type_MainWindow;
			    
			    return false;
			});*/

			
		}
		private void on_menuSettingBtn_activate(object sender , EventArgs e)
        {

          


        }
        
        
        
    }
}