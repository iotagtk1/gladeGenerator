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
            if (SelectedGladeDataRow == null || 
                SelectedTopLevelPartRow == null ||
                SelectedChildLevelPartRow == null ||
                ((TopLevelPart)SelectedTopLevelPartRow).PartId == "" || 
                ((ChildLevelPart)SelectedChildLevelPartRow).PartId == "")
            {
                Console.WriteLine(" 例外 TopLevelPartChildPartKeyがない ");
                return "";
            }

            string GladeName = ((GladeData)SelectedGladeDataRow).GladeName._md5(); 
            string topLevelPartKey = ((TopLevelPart)SelectedTopLevelPartRow).PartId._md5();
            string childLevelPartKey = ((ChildLevelPart)SelectedChildLevelPartRow).PartId._md5();

            return GladeName + topLevelPartKey + childLevelPartKey;
        }
        
        /// <summary>
        /// Toplevelを生成する
        /// </summary>
        /// <returns></returns>
        private string _getTopLevelPartKey()
        {
            if (SelectedGladeDataRow == null || 
                SelectedTopLevelPartRow == null ||
                ((TopLevelPart)SelectedTopLevelPartRow).PartId == "" )
            {
                Console.WriteLine(" 例外 TopLevelPartKeyがない ");
                return "";
            }

            string GladeName = ((GladeData)SelectedGladeDataRow).GladeName._md5(); 
            string topLevelPartKey = ((TopLevelPart)SelectedTopLevelPartRow).PartId._md5();

            return GladeName + topLevelPartKey ;
        }
        
        
        
    }
}