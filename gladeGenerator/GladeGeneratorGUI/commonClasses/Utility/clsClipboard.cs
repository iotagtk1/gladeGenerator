using System;
using Gdk;
using Gtk;

public class clsClipboard
{
    static public void _setText(string text)
    {
        Gdk.Atom _atom = Gdk.Atom.Intern("CLIPBOARD", false);
        Gtk.Clipboard _clipBoard = Gtk.Clipboard.Get(_atom);

        _clipBoard.SetText(text);

        //テキストをクリップボードへペースト
        // _clipBoard.Text = text;
    }

    static public void _setText(Pixbuf image)
    {
        Gdk.Atom _atom = Gdk.Atom.Intern("CLIPBOARD", false);
        Gtk.Clipboard _clipBoard = Gtk.Clipboard.Get(_atom);

        //テキストをクリップボードへペースト
        _clipBoard.Image = image;
    }

    static public string _getText()
    {
        Gdk.Atom _atom = Gdk.Atom.Intern("CLIPBOARD", false);
        Gtk.Clipboard _clipBoard = Gtk.Clipboard.Get(_atom);

        //クリップボードからテキストをコピー
        if (_clipBoard.WaitIsTextAvailable())
        {
            var text = _clipBoard.WaitForText();
            return text;
        }

        return "";
    }

    static public Pixbuf _getImage()
    {
        Gdk.Atom _atom = Gdk.Atom.Intern("CLIPBOARD", false);
        Gtk.Clipboard _clipBoard = Gtk.Clipboard.Get(_atom);

        //クリップボードからイメージをコピー
        if (_clipBoard.WaitIsImageAvailable())
        {
            var image = _clipBoard.WaitForImage();
            return image;
        }

        return null;
    }
}