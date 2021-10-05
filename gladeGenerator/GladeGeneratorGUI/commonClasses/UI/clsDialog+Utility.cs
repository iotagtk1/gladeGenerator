using System;
using System.Collections.Generic;
using GLib;
using Gtk;

public partial class clsDialog
{
    public static void _addRecentFilter(RecentChooserDialog fc, List<Gtk.RecentFilter> filters)
    {
        if (filters != null && filters.Count > 0)
        {
            foreach (RecentFilter filter in filters)
            {
                fc.AddFilter(filter);
            }
        }
    }

    public static void _setRecentDefuaultFilter(RecentChooserDialog rc)
    {
        RecentFilter filter = new RecentFilter();
        filter.Name = "All files";
        filter.AddPattern("*");
        rc.AddFilter(filter);
    }

    public static void _setFileDefuaultFilter(FileChooserDialog fc)
    {
        FileFilter filter = new FileFilter();
        filter.Name = "All files";
        filter.AddPattern("*");
        fc.AddFilter(filter);
    }

    public static void _addFilter(FileChooserDialog fc, List<Gtk.FileFilter> filters)
    {
        /*
         *	filter.set_name("Graphviz dot files")
            filter.add_pattern("*.dot")
        */
        if (filters != null)
        {
            foreach (FileFilter filter in filters)
            {
                fc.AddFilter(filter);
            }
        }
    }
}