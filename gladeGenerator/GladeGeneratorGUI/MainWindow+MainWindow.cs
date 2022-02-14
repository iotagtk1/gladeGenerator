using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GladeGeneratorGUI
{
    partial class MainWindow
    {

        private GladeData SelectedGladeDataRow = null; 
        private TopLevelPart SelectedTopLevelPartRow = null;
       // private ChildLevelPart SelectedChildLevelPartRow = null;
        private Signal SelectedSignalRow = null;
        private string SelectedGradeKey = "";
        private string SelectedTopChildSignalKey = "";
        private string SelectedTopEnterTextKey = "";
        public static string SelectedTopEnterTextKey_static = "";

        public static string _getSelectedTopEnterTextKey()
        {
            return SelectedTopEnterTextKey_static;
        }
        
        private void on_TopLevelPartTreeViewSection_changed(object sender, EventArgs e)
        {
            TreeIter iter;
            ITreeModel model;
            if (((Gtk.TreeSelection)sender).GetSelected(out model, out iter)){
                TopLevelPart TopLevelPart1 = (TopLevelPart)TopLevelPartListStore.GetValue(iter, 0);
                SelectedTopLevelPartRow = TopLevelPart1;

                SelectedTopEnterTextKey = _getTopLevelPartKey();
                SelectedTopChildSignalKey = _getTopLevelPartKey();
                SelectedTopEnterTextKey_static = _getTopLevelPartKey();

                _initTextFiled(SelectedTopEnterTextKey);

                _mkTreeViewBinding_ChildLevelPart(TopLevelPart1.ChildLevelPartsArray);

                _mkTreeViewBinding_Signal(TopLevelPart1.SignalArray,SelectedTopChildSignalKey);

                foreach (ChildLevelPart ChildLevelPart1 in TopLevelPart1.ChildLevelPartsArray)
                {    var ChildSignalKey = _getTopLevelPartChildPartKey(ChildLevelPart1);
                    _mkTreeViewBinding_Signal(ChildLevelPart1.SignalArray,ChildSignalKey);
                }

                _saveAll(SelectedTopChildSignalKey);
            }
        }
        private void on_ChildLevelPartTreeViewSection_changed(object sender, EventArgs e)
        {
            TreeIter iter;
            ITreeModel model;
            if (((Gtk.TreeSelection)sender).GetSelected(out model, out iter))
            {
                ChildLevelPart ChildLevelPart1 = (ChildLevelPart)ChildLevelPartListStore.GetValue(iter, 0);
                //SelectedChildLevelPartRow = ChildLevelPart1;
                
                var childSignalKey = _getTopLevelPartChildPartKey(ChildLevelPart1);

                SelectedTopChildSignalKey = childSignalKey;

                _mkTreeViewBinding_Signal(ChildLevelPart1.SignalArray,childSignalKey);

                _saveAll(childSignalKey);

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
                _saveAll(SelectedTopChildSignalKey);
            }
        }
         private void on_ListStoreEntry_changed(object sender , EventArgs e){

            string text = ((Gtk.Entry)sender).Text;
            if (SelectedTopEnterTextKey != "")
            {
                clsIniFile.singlton[SelectedTopEnterTextKey,MainWindow.ListStoreEntryStr] = text;
            }

        }
        
        private void on_ModelViewEntry_changed(object sender , EventArgs e){
            
            string text = ((Gtk.Entry)sender).Text;
            if (SelectedTopEnterTextKey != "")
            {
                clsIniFile.singlton[SelectedTopEnterTextKey,MainWindow.ModelViewEntryStr] = text;
            }

        }

        private void on_closeBtn_clicked(object sender, EventArgs e)
        {
            clsUtility._appEnd();
        }
        
        private void on_outPutBtn_clicked(object sender, EventArgs e)
        {
            TreeIter iter;
            ITreeModel model;
            if (TopLevelPartTreeViewSection.GetSelected(out model, out iter)){
                TopLevelPart TopLevelPart1 = (TopLevelPart)TopLevelPartListStore.GetValue(iter, 0);

                if (TopLevelPart1.ChildLevelPartsArray.Count == 0)
                {
                    clsDialog._mkMessageDialog_Ok(this,"Confirmation","There is no Child area to write to.",
                        delegate(MessageDialog md, ResponseType result)
                        {
                            md.Destroy();				
                        }
                    );
                    return;
                }
                clsPartsParse1._outPut();
            }else{
            
                clsDialog._mkMessageDialog_Ok(this,"Confirmation","Display the Child area, and then press the Output button.",
                    delegate(MessageDialog md, ResponseType result)
                    {
                        md.Destroy();				
                    }
                );
            }

        }
    
		private void on_menuSettingBtn_activate(object sender , EventArgs e)
        {

            saveWin saveWin1 = new saveWin(this);
            saveWin1.ShowAll();
        }

        private void on_reloadBtn_clicked(object sender , EventArgs e)
        {
            _parsePrjectFolder();
        }

        
    }
}