using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Gdk;
using Gtk;

public static class clsExtensionGtkLabel {

    /// <summary>
    ///  new Color(0, 0, 0)
    /// </summary>
    /// <param name="label"></param>
    /// <param name="Color1"></param>
    static public void _setBackGroundColor(this Gtk.Label label, Gdk.Color Color1)
    {

        label.ModifyBg(StateType.Normal,Color1);

    }
    
    /// <summary>
    /// new Color(0, 0, 0)
    /// </summary>
    /// <param name="label"></param>
    /// <param name="Color1"></param>
    static public void _setFGroundColor(this Gtk.Label label, Gdk.Color Color1)
    {

        label.ModifyFg(StateType.Normal,Color1);

    } 
    

}