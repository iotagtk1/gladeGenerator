using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Gtk;

public static class GtkListStoreExtensions {
    
    /// <summary>
    /// _getToggledRow
    /// </summary>
    /// <param name="listStore"></param>
    /// <param name="args"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    static public T _getToggledRow<T>(this Gtk.ListStore listStore, ToggledArgs args)
    {
        TreeIter iter;
        T modelObj = default(T);
        if (listStore.GetIterFromString(out iter, args.Path))
        {
            modelObj = (T)listStore.GetValue(iter, 0);
            return modelObj;
        }
        return modelObj;
    }
    /// <summary>
    /// _getSelectedRow
    /// </summary>
    /// <param name="listStore"></param>
    /// <param name="args"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    static public object _getListStoreRowChanged<T>(this Gtk.ListStore listStore, RowChangedArgs args)
    {
        TreeIter iter;
        T modelObj = default(T);
        if (listStore.GetIter(out iter, args.Path))
        {
            modelObj = (T)listStore.GetValue(iter, 0);
            return modelObj;
        }
        return modelObj;
    }

    /// <summary>
    /// ClearCustom
    /// </summary>
    /// <param name="listStore"></param>
    /// <param name="obj"></param>
    /// <param name="columnName"></param>
    /// <typeparam name="T"></typeparam>
    static public void _ClearCustom(this Gtk.ListStore listStore)
    {
        for (int i = 0; i < listStore.IterNChildren(); i++) {
            TreeIter iter;
            if (listStore.IterNthChild(out iter, i))
            {
                listStore.Remove(ref iter);
            }
        }
        if (listStore.IterNChildren() > 0)
        {
            TreeIter iter;
            if (listStore.IterNthChild(out iter, 0))
            {
                listStore.Remove(ref iter);
            }
        }
    }

    /// <summary>
    /// 先頭の前に追加する
    /// </summary>
    /// <param name="listStore"></param>
    /// <param name="obj"></param>
    /// <param name="columnName"></param>
    /// <typeparam name="T"></typeparam>
    static public void _addFirstRow<T>(this Gtk.ListStore listStore,T obj,int columnName = 0)
    {

        if (listStore.IterNChildren() == 0)
        {
            listStore.AppendValues(obj);
            return;
        }
        
        Gtk.TreeIter iterFirst;
        //先頭のIterを取得する
        listStore.GetIterFirst(out iterFirst);
        //先頭の前のIterを取得する
        Gtk.TreeIter iter = listStore.InsertBefore(iterFirst);
        //Liststoreにセットする
        listStore.SetValue(iter, columnName, obj);
    }

    /// <summary>
    /// 最後に追加
    /// </summary>
    /// <param name="listStore"></param>
    /// <param name="obj"></param>
    /// <param name="columnNum"></param>
    /// <typeparam name="T"></typeparam>
    static public void _addRow<T>(this Gtk.ListStore listStore, T obj, int columnNum = 0)
    {
        Gtk.TreeIter iter = listStore.Append();
        listStore.SetValue(iter, columnNum, obj);
    }

    /// <summary>
    /// _addDataArray
    /// </summary>
    /// <param name="list"></param>
    /// <param name="dataArray"></param>
    /// <typeparam name="T"></typeparam>
    static public void _addDataArray<T>(this Gtk.ListStore list, List<T> dataArray)
    {
        foreach (T a  in dataArray)
        {
            list.AppendValues(a);
        }
    }
    
    /// <summary>
    /// getData
    /// </summary>
    /// <param name="list"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    static public List<T> _getData1<T>(this Gtk.ListStore listStore)
    {
        List<T> objectArray = new List<T>();

        listStore.Foreach((model, path, iter) =>{
            T object1 = (T)model.GetValue(iter, 0);
            objectArray.Add(object1);
            return false;
        });
        return objectArray;
    }

    /// <summary>
    /// Count
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    static public int _count(this Gtk.ListStore list)
    {
       return list.IterNChildren();
    }
        
}