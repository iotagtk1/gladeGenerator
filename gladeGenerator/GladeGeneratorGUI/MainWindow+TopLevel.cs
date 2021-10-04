using System;
using System.Collections.Generic;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GladeGeneratorGUI
{
    partial class MainWindow
    {
        Gtk.ListStore TopLevelPartListStore = new Gtk.ListStore(typeof(TopLevelPart));

        public void _mkTreeView_TopLevelPart(GladeData gladeData1)
        {

            SelectedGladeDataRow = gladeData1;

            List<TopLevelPart> TopLevelPartArray = gladeData1.TopLevelPartArray;
            
            Gtk.TreeViewColumnEx ClassNameColumn = new Gtk.TreeViewColumnEx();
            ClassNameColumn.Title = "ClassName";
            ClassNameColumn._mkCellRendererText(TopLevelPartTreeView, "", 100);
            ClassNameColumn.bindingPropertyName = "ClassName";

            foreach (TopLevelPart TopLevelPart1 in TopLevelPartArray)
            {
                TopLevelPartListStore.AppendValues(TopLevelPart1);
            }

            TopLevelPartTreeView.Model = TopLevelPartListStore;

            TopLevelPartTreeView._mkBinding();
        }
    }
}