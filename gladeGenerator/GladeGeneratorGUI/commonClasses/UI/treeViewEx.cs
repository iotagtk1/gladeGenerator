using System;
using System.Reflection;
using Gtk;

namespace Gtk
{
    static class GtkExtensions
    {
        public static void _mkBinding(this TreeView treeView)
        {
            foreach (TreeViewColumnEx column in treeView.Columns)
            {
                if (!(column is TreeViewColumnEx))
                {
                    return;
                }

                TreeViewColumnEx columnt1 = (column as TreeViewColumnEx);
                columnt1._mkBinding();
            }
        }
    }
}