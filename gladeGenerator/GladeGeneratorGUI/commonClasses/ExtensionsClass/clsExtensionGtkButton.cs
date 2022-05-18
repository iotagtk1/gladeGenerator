using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Gtk;

public static class GtkButtonExtensions {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="btn"></param>
    /// <returns></returns>
    static public string _addZeroNum(this Gtk.SpinButton btn)
    {
        string num = btn.ValueAsInt.ToString();
        if (num.Length == 1)
        {
            num = "0" + num;
        }
        return num;
    }
    
    /// <summary>
    /// _getWidthHeight
    /// </summary>
    /// <param name="btn"></param>
    /// <returns></returns>
    static public Dictionary<int,int> _getWidthHeight(this Gtk.Button btn)
    {
        int width;
        int height;
        btn.GetSizeRequest(out width,out height);
        return  new Dictionary<int, int>() {{ width, height} };
    }
    
    /// <summary>
    /// _getWidth
    /// </summary>
    /// <param name="btn"></param>
    /// <returns></returns>
    static public int _getWidth(this Gtk.Button btn)
    {
        int width;
        int height;
        btn.GetSizeRequest(out width,out height);
        return width;
    }
    
    /// <summary>
    /// _getHeight
    /// </summary>
    /// <param name="btn"></param>
    /// <returns></returns>
    static public int _getHeight(this Gtk.Button btn)
    {
        int width;
        int height;
        btn.GetSizeRequest(out width,out height);
        return height;
    }
    
    /// <summary>
    /// _setWidthHeight
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    static public void _setWidthHeight(this Gtk.Button btn,int width,int height)
    {
        btn.SetSizeRequest(width, height);
    }
    
    

}