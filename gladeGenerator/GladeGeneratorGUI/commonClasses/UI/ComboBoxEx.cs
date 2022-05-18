using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Gdk;
using Gtk;

namespace Gtk
{
    public static partial class ComboxExtensions
    {
        static public void _comboInit(ComboBox Combox1)
        {
            Combox1.Clear();
        }

        static public Gtk.CellRendererText _mkCellRendererText(this ComboBox Combox, string bindName)
        {
            _comboInit(Combox);

            Gtk.CellRendererTextEx CellRendererTextEx1 = new Gtk.CellRendererTextEx();
            CellRendererTextEx1.BindingPropertyName = bindName;

            Combox.PackStart(CellRendererTextEx1, false);

            return CellRendererTextEx1;
        }

        static public void _mkBinding(this ComboBox Combox1)
        {
            if (Combox1.Cells.Length > 0)
            {
                Combox1.SetCellDataFunc(Combox1.Cells[0], new Gtk.CellLayoutDataFunc(_RenderComboDo));
            }
        }

        static private void _RenderComboDo(
            Gtk.ICellLayout cell_layout,
            Gtk.CellRenderer cell,
            Gtk.ITreeModel model,
            Gtk.TreeIter iter)
        {
            if (!(cell is Gtk.CellRendererTextEx))
            {
                return;
            }

            if ((cell as Gtk.CellRendererTextEx).BindingPropertyName == "" ||
                (cell as Gtk.CellRendererTextEx).BindingPropertyName == null)
            {
                Console.WriteLine("PropertyNameがない");
                return;
            }

            object modelobj = (object)model.GetValue(iter, 0);

            object value = modelobj._performSelector_Property((cell as Gtk.CellRendererTextEx).BindingPropertyName);

            if (value != null && cell is Gtk.CellRendererText && (value is String))
            {
                (cell as Gtk.CellRendererText).Text = value as String;
            }
            else if (value != null && cell is Gtk.CellRendererText && (value is int))
            {
                (cell as Gtk.CellRendererText).Text = ((int)value).ToString();
            }
            else if (value != null && cell is Gtk.CellRendererText && (value is long))
            {
                (cell as Gtk.CellRendererText).Text = ((long)value).ToString();
            }
            else if (value != null && cell is Gtk.CellRendererPixbuf && (value is String))
            {
                (cell as Gtk.CellRendererPixbuf).Pixbuf = new Pixbuf((value as String));
            }
            else if (value != null && cell is Gtk.CellRendererToggle && (value is String))
            {
                (cell as Gtk.CellRendererToggle).Active = Convert.ToBoolean((value is String));
            }
            else if (value != null && cell is Gtk.CellRendererProgress && (value is String))
            {
                (cell as Gtk.CellRendererProgress).Value = Convert.ToInt32((value is String));
            }
            else if (value != null && cell is Gtk.CellRendererPixbuf && (value is byte[]))
            {
                (cell as Gtk.CellRendererPixbuf).Pixbuf = new Pixbuf((byte[])value);
            }
            else if (value != null && cell is Gtk.CellRendererToggle && (value is Boolean))
            {
                (cell as Gtk.CellRendererToggle).Active = (Boolean)value;
            }
            else if (value != null && cell is Gtk.CellRendererProgress && (value is int))
            {
                (cell as Gtk.CellRendererProgress).Value = (int)value;
            }
            else if (value != null && cell is Gtk.CellRendererProgress && (value is long))
            {
                (cell as Gtk.CellRendererProgress).Value = (int)value;
            }
        }
    }
}