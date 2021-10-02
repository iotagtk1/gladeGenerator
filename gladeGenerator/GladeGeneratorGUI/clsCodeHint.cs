using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace GladeGeneratorGUI
{
    public class clsCodeHint
    {
        private static clsCodeHint _singleInstance = null;

        public static clsCodeHint Instance()
        {
            if (_singleInstance == null)
            {
                _singleInstance = new clsCodeHint();
            }

            return _singleInstance;
        }

        private XmlDocument CodeHintXmlDoc = null;

        private clsCodeHint()
        {
            try
            {
                string UICodeHintPath = clsFile._getExePath_replace("codeHint/UICodeHint.xml");
                CodeHintXmlDoc = new XmlDocument();
                CodeHintXmlDoc.Load(UICodeHintPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// コードヒントを検索する
        /// </summary>
        /// <param name="uiName"></param>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public string _searchUIHintCode(string uiName, string eventName)
        {
            if (!clsArgsConfig.Instance().isCodeHint)
            {
                return "";
            }

            string selecterStr =
                "//uiCodeHint/codeHint[@uiName = '" + uiName + "' and @eventName= '" + eventName + "']";

            if (uiName == "GtkListStore")
            {
                //Console.WriteLine("GtkListStore");
            }

            XmlNode xmlNode1 = CodeHintXmlDoc.SelectSingleNode(selecterStr);
            if (xmlNode1 != null)
            {
                if (xmlNode1._getAttributesValue("isOutPut")._boolValue())
                {
                    string filePath = xmlNode1._getAttributesValue("filePath");
                    if (filePath != "")
                    {
                        string filePath2 = clsArgsConfig.Instance().codeHitFolder + "/" + filePath;
                        string uiCodeHintPath = clsFile._getExePath_replace(filePath2);
                        string condeHint = clsFile._load_static(uiCodeHintPath);

                        string lineNew = "";
                        foreach (string str in condeHint._split(Environment.NewLine))
                        {
                            lineNew += "\t\t\t" + str + Environment.NewLine;
                        }

                        return lineNew;
                    }
                }
            }

            return "";
        }
    }
}