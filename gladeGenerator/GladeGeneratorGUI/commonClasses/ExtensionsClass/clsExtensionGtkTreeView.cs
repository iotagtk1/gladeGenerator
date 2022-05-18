using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Gtk;

public static class clsExtensionGtkTreeView {

    
    /// <summary>
    /// _getSelectedRow
    /// </summary>
    /// <param name="treeView"></param>
    /// <param name="listStore"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    static public T _getSelectionSelectedRow<T>(this Gtk.TreeView treeView,Gtk.ListStore listStore)
    {
        TreeIter iter;
        ITreeModel model;
        T modelData = default(T);
        if (treeView.Selection.GetSelected(out model,out iter))
        {
            modelData = (T)listStore.GetValue(iter, 0);
            return modelData;
        }
        return modelData;
    }

    /// <summary>
    /// _deleteSelectedRow
    /// </summary>
    /// <param name="treeView"></param>
    /// <param name="listStore"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    static public void _deleteSelectedRow<T>(this Gtk.TreeView treeView, Gtk.ListStore listStore)
    {
        TreeIter iter;
        ITreeModel model;
        if (treeView.Selection.GetSelected(out model,out iter))
        {
            listStore.Remove(ref iter);
        }
    }

    /// <summary>
    /// _unSelectAll
    /// </summary>
    /// <param name="treeView"></param>
    static public void _unSelectAll(this Gtk.TreeView treeView)
    {
        treeView.Selection.UnselectAll();
    }

    /// <summary>
    /// _getSelected
    /// </summary>
    /// <param name="treeView"></param>
    /// <returns></returns>
    static public bool _getSelected(this Gtk.TreeView treeView)
    {
        TreeIter iter;
        ITreeModel model;
        if (treeView.Selection.GetSelected(out model, out iter))
        {
            return true;
        }
        return false;
    }

    
    

}