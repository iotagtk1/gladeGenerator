using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Gdk;
using Gtk;

namespace Gtk
{
    public partial class CellRendererTextEx : Gtk.CellRendererText
    {
        public string BindingPropertyName = "";
    }
}