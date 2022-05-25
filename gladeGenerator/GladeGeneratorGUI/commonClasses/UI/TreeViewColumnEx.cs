using System;
using System.Reflection;
using Gdk;
using Gtk;

namespace Gtk
{
    public class TreeViewColumnEx : TreeViewColumn
    {
        public String bindingPropertyName = "";
        private Gtk.ListStore listStore1 = null;

        public CellRendererText _mkCellRendererText(TreeView treeView, string title = "", int minWidth = 0, int maxWidth = 0,
            bool isEditable = true, bool isExpand = false, bool isPackStart = true , bool isAutoEdit = true,bool isAutoSize = false)
        {
            if (title != "")
            {
                this.Title = title;
            }
            Gtk.CellRendererTextEx CellRendererText1 = new Gtk.CellRendererTextEx();
            if (minWidth != 0)
            {
                this.MinWidth = minWidth;
            }
            if (maxWidth != 0)
            {
                this.MaxWidth = maxWidth;
            }      

            this.Expand = isExpand;
            this.Sizing = isAutoSize ? TreeViewColumnSizing.Autosize : TreeViewColumnSizing.Fixed;
            this.PackStart(CellRendererText1, isPackStart);
            listStore1 = (ListStore)treeView.Model;
            if (isEditable)
            {
                CellRendererText1.Editable = isAutoEdit;
                CellRendererText1.Edited += delegate(object o, EditedArgs args)
                {
                    Gtk.CellRendererTextEx o1 = (Gtk.CellRendererTextEx)o;
                    TreePath treePath1 = new TreePath(args.Path);
                    TreeIter iter;
                    if (listStore1 != null)
                    {
                        listStore1.GetIter(out iter, treePath1);
                        object testModel1 = (object)listStore1.GetValue(iter, 0);
                        _setModelData(testModel1, bindingPropertyName, args.NewText);
                    }
                };
            }

            treeView.AppendColumn(this);
            return CellRendererText1;
        }

        public CellRendererPixbuf _mkCellRendererPixbuf(TreeView treeView, string title = "",int minWidth = 0,int maxWidth = 0,
            bool isExpand = false, bool isPackStart = true,bool isAutoSize = false)
        {
            if (title != "")
            {
                this.Title = title;
            }

            Gtk.CellRendererPixbuf CellRendererPixbuf1 = new Gtk.CellRendererPixbuf();

            if (minWidth != 0)
            {
                this.MinWidth = minWidth;
            }
            if (maxWidth != 0)
            {
                this.MaxWidth = maxWidth;
            }

            this.Expand = isExpand;
            this.Sizing = isAutoSize ? TreeViewColumnSizing.Autosize : TreeViewColumnSizing.Fixed;
            listStore1 = (ListStore)treeView.Model;
            this.PackStart(CellRendererPixbuf1, isPackStart);
            //this.AddAttribute (CellRendererPixbuf1, "pixbuf", 0);  
            treeView.AppendColumn(this);
            return CellRendererPixbuf1;
        }

        public CellRendererToggle _mkCellRendererToggle(TreeView treeView, string title = "", int minWidth = 0,int maxWidth = 0,
            bool isToggled = false, bool isExpand = false, bool isPackStart = true,bool isAutoSize = false)
        {
            if (title != "")
            {
                this.Title = title;
            }

            Gtk.CellRendererToggle CellRendererToggle1 = new Gtk.CellRendererToggle();
            if (minWidth != 0)
            {
                this.MinWidth = minWidth;
            }
            if (maxWidth != 0)
            {
                this.MaxWidth = maxWidth;
            }
            this.Expand = isExpand;
            this.Sizing = isAutoSize ? TreeViewColumnSizing.Autosize : TreeViewColumnSizing.Fixed;
            listStore1 = (ListStore)treeView.Model;
            if (isToggled)
            {
                CellRendererToggle1.Toggled += delegate(object o, ToggledArgs args)
                {
                    TreeIter iter;
                    if (listStore1.GetIterFromString(out iter, args.Path))
                    {
                        object object1 = (object)listStore1.GetValue(iter, 0);
                        object value = object1._performSelector_Property(bindingPropertyName);
                        String val = Convert.ToBoolean(value) == true ? "false" : "true";
                        _setModelData(object1, bindingPropertyName, val);
                    }
                };
            }

            this.PackStart(CellRendererToggle1, isPackStart);
            treeView.AppendColumn(this);
            return CellRendererToggle1;
        }

        public CellRendererProgress _mkCellRendererProgress(TreeView treeView, string title = "" , 
            int minWidth = 0 , int maxWidth = 0, bool isExpand = false, bool isPackStart = true,bool isAutoSize = false)
        {
            if (title != "")
            {
                this.Title = title;
            }

            Gtk.CellRendererProgress CellRendererProgress1 = new Gtk.CellRendererProgress();
            if (minWidth != 0)
            {
                this.MinWidth = minWidth;
            }
            if (maxWidth != 0)
            {
                this.MaxWidth = maxWidth;
            }

            this.Expand = isExpand;
            this.Sizing = isAutoSize ? TreeViewColumnSizing.Autosize : TreeViewColumnSizing.Fixed;
            listStore1 = (ListStore)treeView.Model;
            this.PackStart(CellRendererProgress1, isPackStart);
            treeView.AppendColumn(this);
            return CellRendererProgress1;
        }

        public void _mkBinding()
        {
            if (this.Cells.Length > 0)
            {
                this.SetCellDataFunc(this.Cells[0], new Gtk.TreeCellDataFunc(_RenderCellDo));
            }
        }

        private void _RenderCellDo(Gtk.TreeViewColumn column, Gtk.CellRenderer cell,
            Gtk.ITreeModel model, Gtk.TreeIter iter)
        {
            try
            {
                if (!(column is TreeViewColumnEx))
                {
                    Console.WriteLine("_RenderCellDo");
                    return;
                }
                TreeViewColumnEx column1 = (column as TreeViewColumnEx);
                if (column1.bindingPropertyName == null || column1.bindingPropertyName == "")
                {
                    Console.WriteLine("PropertyNameがない");
                    return;
                }  
                object modelData = (object)model.GetValue(iter, 0);
                object value = modelData._performSelector_Property(column1.bindingPropertyName);
                _setCellData(value, cell);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }
        }

        private void _setCellData(object value, Gtk.CellRenderer cell)
        {
            if (value != null && cell is Gtk.CellRendererText && (value is String))
            {
                (cell as Gtk.CellRendererText).Text = value as String;
            }
            else if (value != null && cell is Gtk.CellRendererText && (value is int))
            {
                (cell as Gtk.CellRendererText).Text = ((int)value).ToString();
            }else if (value != null && cell is Gtk.CellRendererText && (value is double))
            {
                (cell as Gtk.CellRendererText).Text = ((double)value).ToString();
            }else if (value != null && cell is Gtk.CellRendererText && (value is long))
            {
                (cell as Gtk.CellRendererText).Text = ((long)value).ToString();
            }
            else if (value != null && cell is Gtk.CellRendererText && (value is DateTime))
            {
                (cell as Gtk.CellRendererText).Text = ((DateTime)value).ToString();
            }
            else if (value != null && cell is Gtk.CellRendererPixbuf && (value is String))
            {
                (cell as Gtk.CellRendererPixbuf).Pixbuf = new Pixbuf(null, (value as String));
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
        }

        private void _setModelData(object modelData1, String bindingPropertyName1, String value)
        {
            Type t = modelData1._getKata(bindingPropertyName1);
            if (value != null && t.Equals(typeof(String)))
            {
                modelData1._setSelector_Property(bindingPropertyName1, Convert.ToString(value));
            }
            else if (value != null && t.Equals(typeof(int)))
            {
                modelData1._setSelector_Property(bindingPropertyName1, Convert.ToInt32(value));
            }
            else if (value != null && t.Equals(typeof(double)))
            {
                modelData1._setSelector_Property(bindingPropertyName1, Convert.ToDouble(value));
            }
            else if (value != null && t.Equals(typeof(long)))
            {
                modelData1._setSelector_Property(bindingPropertyName1, Convert.ToInt64(value));
            }
            else if (value != null && t.Equals(typeof(Boolean)))
            {
                modelData1._setSelector_Property(bindingPropertyName1, Convert.ToBoolean(value));
            }
            else if (value != null && t.Equals(typeof(bool)))
            {
                modelData1._setSelector_Property(bindingPropertyName1, Convert.ToBoolean(value));
            }
            else if (value != null && t.Equals(typeof(DateTime)))
            {
                modelData1._setSelector_Property(bindingPropertyName1, Convert.ToDateTime(value));
            }
            else if (value != null && t.Equals(typeof(decimal)))
            {
                modelData1._setSelector_Property(bindingPropertyName1, Convert.ToDecimal(value));
            }
            else if (value != null && t.Equals(typeof(char)))
            {
                modelData1._setSelector_Property(bindingPropertyName1, Convert.ToChar(value));
            }
            else if (value != null && t.Equals(typeof(byte)))
            {
                modelData1._setSelector_Property(bindingPropertyName1, Convert.ToByte(value));
            }
        }
    }
}