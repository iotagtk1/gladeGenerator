using System;
using System.Collections.Generic;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GladeGeneratorGUI
{
    partial class MainWindow
    {
        Gtk.ListStore TopLevelPartListStore = new Gtk.ListStore(typeof(TopLevelPart));

        public void _mkTreeView_TopLevelPart() {

            Gtk.TreeViewColumnEx ClassNameColumn = new Gtk.TreeViewColumnEx();
            ClassNameColumn.Title = "Top";
            ClassNameColumn._mkCellRendererText(TopLevelPartTreeView, "", 145);
            ClassNameColumn.bindingPropertyName = "PartId";
            
            Gtk.TreeViewColumnEx isReOutPutColumn = new Gtk.TreeViewColumnEx();
            isReOutPutColumn.Title = "OutPut";
            isReOutPutColumn.bindingPropertyName = "isReOutPut";
            Gtk.CellRendererToggle isReOutPutColumnToggle =
                isReOutPutColumn._mkCellRendererToggle(TopLevelPartTreeView, "", 60);
            isReOutPutColumnToggle.Toggled += delegate(object o, ToggledArgs args)
            {
                TreeIter iter;
                if (TopLevelPartListStore.GetIterFromString(out iter, args.Path))
                {
                    TopLevelPart TopLevelPart1 = (TopLevelPart)TopLevelPartListStore.GetValue(iter, 0);
                    TopLevelPart1.isReOutPut = TopLevelPart1.isReOutPut == true ? false : true;
                    _saveAll_Top(SelectedGradeKey);
                }
            };

        }
        
        public void _mkTreeViewBinding_TopLevelPart(GladeData gladeData1)
        {
            
            dataGladeTopLevelPartDic = clsFile._getJsonData<Dictionary<string, List<TopLevelPart>>>(saveGladeTopPartDataFilePath);

            if (dataGladeTopLevelPartDic == null)
            {
                dataGladeTopLevelPartDic = new Dictionary<string, List<TopLevelPart>>();
            }
            
            TopLevelPartListStore = new Gtk.ListStore(typeof(TopLevelPart));
            ChildLevelPartListStore = new ListStore(typeof(ChildLevelPart));
            SignalListStore = new ListStore(typeof(Signal));
            ChildLevelPartTreeView.Model = null;
            SignalTreeView.Model = null;
            
            SelectedGladeDataRow = gladeData1;
            SelectedGradeKey = _getGradeKey();
            
            List<TopLevelPart> TopLevelPartArray = gladeData1.TopLevelPartArray;

            List<TopLevelPart> TopLevelPart_OldArray = new List<TopLevelPart>();
            if (dataGladeTopLevelPartDic != null && dataGladeTopLevelPartDic.ContainsKey(SelectedGradeKey) && 
                dataGladeTopLevelPartDic[SelectedGradeKey] != null)
            {
                TopLevelPart_OldArray = dataGladeTopLevelPartDic[SelectedGradeKey];
            }

            foreach (TopLevelPart TopLevelPart1 in TopLevelPartArray)
            {
                foreach (TopLevelPart TopLevelPart_old in TopLevelPart_OldArray)
                {
                    if (TopLevelPart1.PartId == TopLevelPart_old.PartId)
                    {
                        TopLevelPart1.isReOutPut = TopLevelPart_old.isReOutPut;
                        break;
                    }
                }
                TopLevelPartListStore.AppendValues(TopLevelPart1);
            }
  
            TopLevelPartTreeView.Model = TopLevelPartListStore;

            TopLevelPartTreeView._mkBinding();
        }
        
        private Dictionary<string, List<TopLevelPart>> dataGladeTopLevelPartDic = null;
        private void _saveAll_Top(string GradeKey)
        {

            if ( GradeKey == null || GradeKey == "")
            {
                Console.WriteLine("keyがない");
                return;
            }

            List<TopLevelPart> TopLevelPartArray = new List<TopLevelPart>();
 
            TopLevelPartListStore.Foreach((model, path, iter) =>{
                TopLevelPart testModel1 = model.GetValue(iter, 0) as TopLevelPart;    
                TopLevelPartArray.Add(testModel1);
                return false;
            });
		    
            if (dataGladeTopLevelPartDic.ContainsKey(GradeKey))
            {
                dataGladeTopLevelPartDic[GradeKey] = TopLevelPartArray;
            }
            else
            {
                dataGladeTopLevelPartDic.Add(GradeKey,TopLevelPartArray);
            }
            
            if (dataGladeTopLevelPartDic != null)
            {
                clsFile._saveJsonData<Dictionary<string, List<TopLevelPart>>>(saveGladeTopPartDataFilePath, dataGladeTopLevelPartDic);
            }
        }

    }
}