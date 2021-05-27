using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml;

namespace signalGenerator
{

    public partial class clsGtkXmlParse
    {

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