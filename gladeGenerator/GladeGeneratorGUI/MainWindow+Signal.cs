using System;
using System.Collections.Generic;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GladeGeneratorGUI
{
    partial class MainWindow
    {
        Gtk.ListStore SignalListStore = new Gtk.ListStore(typeof(Signal));
        
        private void _mkTreeView_Signal(List<Signal> SignalArray ,string DorTKey)
        {
            Gtk.TreeViewColumnEx EventNameColumn = new Gtk.TreeViewColumnEx();
            EventNameColumn.Title = "EventName";
            EventNameColumn._mkCellRendererText(SignalTreeView, "", 100);
            EventNameColumn.bindingPropertyName = "EventName";

            Gtk.TreeViewColumnEx HandlerNameColumn = new Gtk.TreeViewColumnEx();
            HandlerNameColumn.Title = "HandlerName";
            HandlerNameColumn._mkCellRendererText(SignalTreeView, "", 100);
            HandlerNameColumn.bindingPropertyName = "HandlerName";

            Gtk.TreeViewColumnEx isReOutPutColumn = new Gtk.TreeViewColumnEx();
            isReOutPutColumn.Title = "isReOutPut";
            isReOutPutColumn.bindingPropertyName = "isReOutPut";
            Gtk.CellRendererToggle isReOutPutColumnToggle =
                isReOutPutColumn._mkCellRendererToggle(SignalTreeView, "", 60);
            isReOutPutColumnToggle.Toggled += delegate(object o, ToggledArgs args)
            {
                TreeIter iter;
                if (SignalListStore.GetIterFromString(out iter, args.Path))
                {
                    Signal Signal1 = (Signal)SignalListStore.GetValue(iter, 0);
                    Signal1.isReOutPut = Signal1.isReOutPut == true ? false : true;
                }
            };
            
            dataSignalDic = clsFile._getJsonData<Dictionary<string, List<Signal>>>(saveDataFilePath);

            if (dataSignalDic == null)
            {
                dataSignalDic = new Dictionary<string, List<Signal>>();
            }

            List<Signal> Signal_OldArray = new List<Signal>();
            if (dataSignalDic != null && dataSignalDic.ContainsKey(DorTKey) && 
                dataSignalDic[DorTKey] != null)
            {
                Signal_OldArray = dataSignalDic[DorTKey];
            }

            foreach (Signal Signal1 in SignalArray)
            {
                
                foreach (Signal Signal_old in Signal_OldArray)
                {
                    if (Signal_old.EventName == Signal1.EventName)
                    {
                        Signal1.isReOutPut = Signal_old.isReOutPut;
                        break;
                    }
                }
                
                SignalListStore.AppendValues(Signal1);
            }

            SignalTreeView.Model = SignalListStore;

            SignalTreeView._mkBinding();
        }
        
        private Dictionary<string, List<Signal>> dataSignalDic = null;
        
        /// <summary>
        /// Columnの状態を保存する
        /// </summary>
        private void _saveAll(string dbKeyOrTableKey)
        {

            if ( SelectedDbTableKey == null || SelectedDbTableKey == "")
            {
                Console.WriteLine("keyがない");
                return;
            }

            List<Signal> SignalModelArray = new List<Signal>();
            
            SignalListStore.Foreach((model, path, iter) =>{
                Signal testModel1 = model.GetValue(iter, 0) as Signal;    
                SignalModelArray.Add(testModel1);
                return false;
            });
		    
            if (dataSignalDic.ContainsKey(dbKeyOrTableKey))
            {
                dataSignalDic[dbKeyOrTableKey] = SignalModelArray;
            }
            else
            {
                dataSignalDic.Add(dbKeyOrTableKey,SignalModelArray);
            }

            if (dataSignalDic != null)
            {
                clsFile._saveJsonData<Dictionary<string, List<Signal>>>(saveDataFilePath, dataSignalDic);
            }

        }

        
        
        
        
        
        
        
    }
}