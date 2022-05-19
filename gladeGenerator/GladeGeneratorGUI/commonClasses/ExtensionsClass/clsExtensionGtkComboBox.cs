using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Gtk;

public static class clsExtensionGtkComboBox {

    
    /// <summary>
    /// _getSelectedRow
    /// </summary>
    /// <param name="comboBox"></param>
    /// <param name="listStore"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    static public T _getSelectedRow<T>(this Gtk.ComboBox comboBox,Gtk.ListStore listStore)
    {
       
        TreeIter tree;
        comboBox.GetActiveIter(out tree);
        T modelObj = (T)listStore.GetValue (tree, 0);
        
        return modelObj;
    }


    /// <summary>
    /// _isSelected
    /// </summary>
    /// <param name="comboBox"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    static public bool _isSelected(this Gtk.ComboBox comboBox)
    {
        if (comboBox.Active == -1)
        {
            return false;
        }
        
        return true;
    }

   


}