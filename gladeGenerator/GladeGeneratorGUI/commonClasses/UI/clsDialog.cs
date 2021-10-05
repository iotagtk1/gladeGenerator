using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Gdk;
using GLib;
using Gtk;
using Window = Gtk.Window;

/*
 *
	clsDialog._mkMessageDialog_Ok(this,"","",
		delegate(MessageDialog md, ResponseType result)
		{
			if (result == ResponseType.Yes)
				Application.Quit();
			else
				md.Destroy();
	});
 * 
 */

public partial class clsDialog
{
    public static void _mkColorSelectionDialog_Ok(Window win, string title,
        Color currentColor, Color preColor,
        Action<ColorSelectionDialog, ResponseType> func)
    {
        if (title == "")
        {
            title = "Change Color";
        }

        Gtk.ColorSelectionDialog cs = new ColorSelectionDialog(title);
        cs.Parent = win;
        cs.ColorSelection.HasPalette = true;

        if (!currentColor.Equals(null))
        {
            cs.ColorSelection.CurrentColor = currentColor;
        }

        if (!preColor.Equals(null))
        {
            cs.ColorSelection.PreviousColor = preColor;
        }

        Gtk.ResponseType retult1 = (ResponseType)cs.Run();
        func(cs, retult1);
    }

    public static void _mkAppChooserDialog_AcceptCancel(Window win, string title,
        Action<AppChooserDialog, ResponseType> func)
    {
        Gtk.AppChooserDialog ac = new AppChooserDialog(IntPtr.Zero);
        ac.Parent = win;
        if (title != "")
        {
            ac.Heading = title;
        }

        Gtk.ResponseType retult1 = (ResponseType)ac.Run();
        func(ac, retult1);
    }


    public static void _mkRecentChooserDialog_AcceptCancel(Window win,
        string title,
        List<Gtk.RecentFilter> filters,
        Action<RecentChooserDialog, ResponseType> func)
    {
        Gtk.RecentChooserDialog rc = new RecentChooserDialog(IntPtr.Zero);

        rc.Parent = win;

        if (title == "")
        {
            title = "Recent Documents";
        }

        rc.Title = title;

        if (filters != null && filters.Count > 0)
        {
            _addRecentFilter(rc, filters);
        }

        _setRecentDefuaultFilter(rc);

        Gtk.ResponseType retult1 = (ResponseType)rc.Run();

        func(rc, retult1);
    }

    public static void _mkLoadFileDialog_AcceptCancel(Window win, string title,
        List<FileFilter> filters, Action<FileChooserDialog, ResponseType> func)
    {
        if (title == "")
        {
            title = "Choose the file to open";
        }

        Gtk.FileChooserDialog fc =
            new Gtk.FileChooserDialog(title,
                win,
                FileChooserAction.Open,
                "Cancel", ResponseType.Cancel,
                "Open", ResponseType.Accept);

        if (filters != null && filters.Count > 0)
        {
            _addFilter(fc, filters);
        }

        _setFileDefuaultFilter(fc);

        Gtk.ResponseType retult1 = (ResponseType)fc.Run();

        func(fc, retult1);
    }

    public static void _mkSaveFileDialog_AcceptCancel(Window win, string title,
        List<Gtk.FileFilter> filters, Action<FileChooserDialog, ResponseType> func)
    {
        if (title == "")
        {
            title = "Choose the file to Save";
        }

        Gtk.FileChooserDialog fc =
            new Gtk.FileChooserDialog(title,
                win,
                FileChooserAction.Save,
                "Cancel", ResponseType.Cancel,
                "Save", ResponseType.Accept);

        if (filters != null && filters.Count > 0)
        {
            _addFilter(fc, filters);
        }

        _setFileDefuaultFilter(fc);

        Gtk.ResponseType retult1 = (ResponseType)fc.Run();

        func(fc, retult1);
    }

    public static void _mkSelectFolderDialog_AcceptCancel(Window win, string title,
        Action<FileChooserDialog, ResponseType> func)
    {
        if (title == "")
        {
            title = "Choose the Folder";
        }

        Gtk.FileChooserDialog fc =
            new Gtk.FileChooserDialog(title,
                win,
                FileChooserAction.SelectFolder,
                "Cancel", ResponseType.Cancel,
                "Save", ResponseType.Accept);

        Gtk.ResponseType retult1 = (ResponseType)fc.Run();

        func(fc, retult1);
    }

    public static void _mkCreateFolderDialog_AcceptCancel(Window win, string title,
        Action<FileChooserDialog, ResponseType> func)
    {
        if (title == "")
        {
            title = "Create the Folder";
        }

        Gtk.FileChooserDialog fc =
            new Gtk.FileChooserDialog(title,
                win,
                FileChooserAction.CreateFolder,
                "Cancel", ResponseType.Cancel,
                "Save", ResponseType.Accept);

        Gtk.ResponseType retult1 = (ResponseType)fc.Run();

        func(fc, retult1);
    }

    public static void _mkMessageDialog_YesNo(Window win, string title, string subTitle,
        Action<MessageDialog, ResponseType> func)
    {
        MessageDialog messageDialog =
            new MessageDialog(win, DialogFlags.Modal, MessageType.Info, ButtonsType.YesNo, title);
        ResponseType retult = (ResponseType)messageDialog.Run();

        if (subTitle != "")
        {
            messageDialog.SecondaryText = subTitle;
        }

        func(messageDialog, retult);
    }

    public static void _mkMessageDialog_Ok(Window win, string title, string subTitle,
        Action<MessageDialog, ResponseType> func)
    {
        MessageDialog messageDialog =
            new MessageDialog(win, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, title);
        ResponseType retult = (ResponseType)messageDialog.Run();

        if (subTitle != "")
        {
            messageDialog.SecondaryText = subTitle;
        }

        func(messageDialog, retult);
    }

    public static void _mkMessageDialog_OkCancel(Window win, string title, string subTitle,
        Action<MessageDialog, ResponseType> func)
    {
        MessageDialog messageDialog =
            new MessageDialog(win, DialogFlags.Modal, MessageType.Info, ButtonsType.OkCancel, title);
        ResponseType retult = (ResponseType)messageDialog.Run();

        if (subTitle != "")
        {
            messageDialog.SecondaryText = subTitle;
        }

        func(messageDialog, retult);
    }

    public static void _mkMessageDialog_Close(Window win, string title, string subTitle,
        Action<MessageDialog, ResponseType> func)
    {
        MessageDialog messageDialog =
            new MessageDialog(win, DialogFlags.Modal, MessageType.Info, ButtonsType.Close, title);
        ResponseType retult = (ResponseType)messageDialog.Run();

        if (subTitle != "")
        {
            messageDialog.SecondaryText = subTitle;
        }

        func(messageDialog, retult);
    }

    public static void _mkMessageDialog_Cancel(Window win, string title, string subTitle,
        Action<MessageDialog, ResponseType> func)
    {
        MessageDialog messageDialog =
            new MessageDialog(win, DialogFlags.Modal, MessageType.Info, ButtonsType.Cancel, title);
        ResponseType retult = (ResponseType)messageDialog.Run();

        if (subTitle != "")
        {
            messageDialog.SecondaryText = subTitle;
        }

        func(messageDialog, retult);
    }
}