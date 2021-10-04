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
            ClassNameColumn._mkCellRendererText(TopLevelPartTreeView, "", 100);
            ClassNameColumn.bindingPropertyName = "PartId";

        }
        
        public void _mkTreeViewBinding_TopLevelPart(GladeData gladeData1)
        {

            TopLevelPartListStore = new Gtk.ListStore(typeof(TopLevelPart));
            
            SelectedGladeDataRow = gladeData1;
            
            List<TopLevelPart> TopLevelPartArray = gladeData1.TopLevelPartArray;

            foreach (TopLevelPart TopLevelPart1 in TopLevelPartArray)
            {
                TopLevelPartListStore.AppendValues(TopLevelPart1);
            }

            TopLevelPartTreeView.Model = TopLevelPartListStore;

            TopLevelPartTreeView._mkBinding();
        }
        
        
        
    }
}