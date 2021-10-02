using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GladeGeneratorGUI
{
    partial class MainWindow
    {

        /// <summary>
        /// ToplevelとChildIdのKeyを生成する
        /// </summary>
        /// <returns></returns>
        private string _getTopLevelPartChildPartKey()
        {
            if (SelectedTopLevelPartRow == null ||
                SelectedChildLevelPartRow == null ||
                ((TopLevelPart)SelectedTopLevelPartRow).PartId == "" || ((ChildLevelPart)SelectedChildLevelPartRow).PartId == "")
            {
                return "";
            }

            string topLevelPartKey = ((TopLevelPart)SelectedTopLevelPartRow).PartId._md5();
            string childLevelPartKey = ((ChildLevelPart)SelectedChildLevelPartRow).PartId._md5();

            return topLevelPartKey + childLevelPartKey;
        }
        
        /// <summary>
        /// Toplevelを生成する
        /// </summary>
        /// <returns></returns>
        private string _getTopLevelPartKey()
        {
            if (SelectedTopLevelPartRow == null ||
                ((TopLevelPart)SelectedTopLevelPartRow).PartId == "" )
            {
                return "";
            }

            string topLevelPartKey = ((TopLevelPart)SelectedTopLevelPartRow).PartId._md5();

            return topLevelPartKey ;
        }
        
        
        
    }
}