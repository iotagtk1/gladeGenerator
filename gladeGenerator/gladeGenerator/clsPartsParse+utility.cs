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

    }
}