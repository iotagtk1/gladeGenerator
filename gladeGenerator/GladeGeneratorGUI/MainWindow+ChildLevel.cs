using System;
using System.Collections.Generic;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GladeGeneratorGUI
{
    partial class MainWindow
    {
        Gtk.ListStore ChildLevelPartListStore = new Gtk.ListStore(typeof(ChildLevelPart));

        private void _mkTreeView_ChildLevelPart(List<ChildLevelPart> ChildLevelPartArray)
        {
            Gtk.TreeViewColumnEx ClassNameColumn = new Gtk.TreeViewColumnEx();
            ClassNameColumn.Title = "ClassName";
            ClassNameColumn._mkCellRendererText(ChildLevelPartTreeView, "", 100);
            ClassNameColumn.bindingPropertyName = "ClassName";

            foreach (ChildLevelPart ChildLevelPart1 in ChildLevelPartArray)
            {
                ChildLevelPartListStore.AppendValues(ChildLevelPart1);
            }

            ChildLevelPartTreeView.Model = ChildLevelPartListStore;

            ChildLevelPartTreeView._mkBinding();
        }
    }
}