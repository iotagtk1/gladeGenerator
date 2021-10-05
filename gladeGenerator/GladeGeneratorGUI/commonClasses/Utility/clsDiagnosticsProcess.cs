using System;
using System.Diagnostics;
using System.IO;
using Gdk;
using Gtk;

public class clsDiagnosticsProcess
{
    /// <summary>
    /// OpenEditを開く
    /// </summary>
    /// <param name="filePath"></param>
    public static void _openGEdit(string filePath)
    {
        if (File.Exists(filePath))
        {
            Process.Start("/usr/bin/gedit", filePath);
        }
    }

    /// <summary>
    /// ディレクトリを開く
    /// </summary>
    /// <param name="DirPath"></param>
    public static void _openDirBroser(string DirPath)
    {
        if (Directory.Exists(DirPath))
        {
            Process.Start("/usr/bin/nautilus", DirPath);
        }
    }
}