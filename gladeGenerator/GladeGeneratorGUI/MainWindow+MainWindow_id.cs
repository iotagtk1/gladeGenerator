using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GladeGeneratorGUI
{
    partial class MainWindow
    {
        //[UI] private readonly Gtk.Window MainWindow = null;
        [UI] private readonly Gtk.Entry TreeViewEntry = null;
        [UI] private readonly Gtk.Entry ListStoreEntry = null;
        [UI] private readonly Gtk.Entry ModelViewEntry = null;
        [UI] private readonly Gtk.Button defualtSetBtn = null;
        [UI] private readonly Gtk.Entry SubNameSpaceEntry = null;
        [UI] private readonly Gtk.Entry ComboViewEntry = null;
        [UI] private readonly Gtk.Button closeBtn = null;
        [UI] private readonly Gtk.Button outPutBtn = null;

        [UI] private readonly Gtk.TreeView TopLevelPartTreeView = null;
        [UI] private readonly Gtk.TreeSelection TopLevelPartTreeViewSection = null;
        [UI] private readonly Gtk.TreeView ChildLevelPartTreeView = null;
        [UI] private readonly Gtk.TreeSelection ChildLevelPartTreeViewSection = null;
        [UI] private readonly Gtk.TreeView SignalTreeView = null;
        [UI] private readonly Gtk.TreeSelection SignalTreeViewSection = null;
    }
}