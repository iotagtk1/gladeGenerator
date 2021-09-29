using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using gladeGenerator;

namespace gladeGenerator
{
    public partial class clsPartsParse
    {
        public Boolean _isNoClassName(XmlNode topObjectNodes)
        {
            Boolean isNoclass = false;
            foreach (string noClassName in NoClassNameArray)
            {
                if (topObjectNodes.Attributes["class"].Value._indexOf(noClassName) != -1)
                {
                    isNoclass = true;
                }
            }

            return isNoclass;
        }
        
        /// <summary>
        /// コメントアウトするクラス
        /// </summary>
        /// <param name="topObjectNodes"></param>
        /// <returns></returns>
        public Boolean _isCommentOutClassName(XmlNode topObjectNodes)
        {
            Boolean isNoclass = false;
            foreach (string noClassName in CommentOutClassArray)
            {
                if (topObjectNodes.Attributes["class"].Value._indexOf(noClassName) != -1)
                {
                    isNoclass = true;
                }
            }

            return isNoclass;
        }

        /// <summary>
        /// イベント名を修正する
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public string _convertEventName(string eventName)
        {
            
            
            
            if (eventName._indexOf("-") != -1)
            {
                ArrayList array = eventName._split("-");
                for (int i = 0; i < array.Count; i++)
                {
                    array[i] = array[i].ToString()._oomojiFirst();
                }
                eventName = array._join("");
                
                return eventName;
            }
            else
            {
                eventName = eventName._oomojiFirst();
            }

            return eventName;
        }
        
        /// <summary>
        /// }}の二重を修正する
        /// </summary>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        public string _kakoFix(string fileContent)
        {

            ArrayList array = fileContent._split(Environment.NewLine);
            if (array.Count == 0)
            {
                return fileContent;
            }
            List<string[]> array2 = array._splitArray(array.Count - 5);
            if (array2 == null || array2.Count < 2 )
            {
                return fileContent;
            }

            string firstStr = string.Join(Environment.NewLine, array2[0]);
            string secondStr =string.Join(Environment.NewLine, array2[1]);

            if (secondStr._indexOf("}}") != -1)
            {
                string replaceStr = "}" + Environment.NewLine + "\t}";
                secondStr = secondStr._replaceReplaceStr("}}", replaceStr);
            }
            if (secondStr._indexOf(";}") != -1)
            {
                string replaceStr = ";" + Environment.NewLine + "\t}";
                secondStr = secondStr._replaceReplaceStr(";}", replaceStr);
            }

            return firstStr + Environment.NewLine + secondStr;
        }

    }
}