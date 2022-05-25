using System;
using System.Collections.Generic;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GladeGeneratorGUI
{
    partial class MainWindow
    {
        Gtk.ListStore SignalListStore = new Gtk.ListStore(typeof(Signal));
        
        private void _mkTreeView_Signal()
        {
            Gtk.TreeViewColumnEx HandlerNameColumn = new Gtk.TreeViewColumnEx();
            HandlerNameColumn.Title = "Signal";
            CellRendererText a = HandlerNameColumn._mkCellRendererText(SignalTreeView, "", 80,maxWidth:100, true,true,true,true);
            HandlerNameColumn.bindingPropertyName = "HandlerName";

            Gtk.TreeViewColumnEx isReOutPutColumn = new Gtk.TreeViewColumnEx();
            isReOutPutColumn.Title = "ReOutPut";
            isReOutPutColumn.bindingPropertyName = "isReOutPut";
            Gtk.CellRendererToggle isReOutPutColumnToggle =
                isReOutPutColumn._mkCellRendererToggle(SignalTreeView, "",75,maxWidth:75);
            isReOutPutColumnToggle.Toggled += delegate(object o, ToggledArgs args)
            {
                TreeIter iter;
                if (SignalListStore.GetIterFromString(out iter, args.Path))
                {
                    Signal Signal1 = (Signal)SignalListStore.GetValue(iter, 0);
                    Signal1.isReOutPut = Signal1.isReOutPut == true ? false : true;
                    _saveAll(SelectedTopChildSignalKey);
                }
            };
            
        }
        
         private void _mkTreeViewBinding_Signal(List<Signal> SignalArray ,string DorTKey)
        {
            
            SignalListStore = new Gtk.ListStore(typeof(Signal));
            
            dataSignalDic = clsFile._getJsonData<Dictionary<string, List<Signal>>>(saveSignalDataFilePath);

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
        private void _saveAll(string TopOrChildKey)
        {

            if ( TopOrChildKey == null || TopOrChildKey == "")
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
		    
            if (dataSignalDic.ContainsKey(TopOrChildKey))
            {
                dataSignalDic[TopOrChildKey] = SignalModelArray;
            }
            else
            {
                dataSignalDic.Add(TopOrChildKey,SignalModelArray);
            }

            if (dataSignalDic != null)
            {
                clsFile._saveJsonData<Dictionary<string, List<Signal>>>(saveSignalDataFilePath, dataSignalDic);
            }

        }


    }
}