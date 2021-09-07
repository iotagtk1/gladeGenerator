using System;
using System.Xml;

namespace gladeGenerator
{
    /*
    * オリジナルsignalデータからデータを取得する
    */
    public class clsSignalsData
    {
        private static clsSignalsData _singleInstance = null;

        public static clsSignalsData Instance()
        {
            if (_singleInstance == null) {
                _singleInstance = new clsSignalsData();
            }
            return _singleInstance;
        }

        private XmlDocument xmlDoc = null;

        private clsSignalsData()
        {
            string filePath = clsFile._getExePath_replace("signals.xml");
            string xmlStr = clsFile._load_static(filePath);
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlStr);
        }

        /// <summary>
        /// Signal.xmlを検索する。callerClassNameからSignalを紐付ける
        /// </summary>
        /// <param name="callerClassName"></param>
        /// <param name="eventName"></param>
        /// <returns></returns>
        
        public SignalTemplateData _searchSignalTemplateData(string callerClassName , string eventName)
        {
            string selectStr = "//root/types[@callerClass='" + callerClassName + "']/signal[@eventName='" + eventName + "' and @callerClass='" + callerClassName +
                               "']";
            
            XmlNodeList signalTemplateNodes = xmlDoc.SelectNodes(selectStr);

            if (signalTemplateNodes == null || signalTemplateNodes.Count == 0)
            {
                return null;
            }

            SignalTemplateData signalTemplateData1 = new SignalTemplateData();

            foreach (XmlNode node in signalTemplateNodes[0].ChildNodes)
            {

                if (node.Name == "method")
                {
                    signalTemplateData1.Method = node.InnerText;
                }else if (node.Name == "args")
                {
                    foreach (XmlNode argsNode in node.ChildNodes)
                    {
                        signalTemplateData1.ArgsArray.Add(argsNode.InnerText);
                    }
                }
            }

            return signalTemplateData1;
        }

        /// <summary>
        /// Signal.xmlをbaseclass検索する
        /// </summary>
        /// <param name="baseClassName"></param>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public SignalTemplateData _searchBaseClassSignalTemplateData(string callerClass , string eventName , 
            ref string errorMes, ref Boolean isReSearch , ref string researchWord)
        {
 
            string selectStr = "//root/types[@callerClass='" + callerClass + "']";
            XmlNodeList typesTemplateNodes = xmlDoc.SelectNodes(selectStr); 
            
            if (typesTemplateNodes == null || typesTemplateNodes.Count == 0)
            {
                isReSearch = false;
                researchWord = "";
                errorMes = string.Format("signals.xmlに{0}がない",callerClass);
                return null;
            }
            
            if (typesTemplateNodes.Count > 0)
            {
                string baseClass = typesTemplateNodes[0].Attributes["baseClass"].Value;
                string selectStr2 = "//root/types[@callerClass='" + baseClass + "']/signal[@eventName='" + eventName + "' and @callerClass='" + baseClass +
                                    "']";
                typesTemplateNodes = xmlDoc.SelectNodes(selectStr2);
                
                if (typesTemplateNodes == null || typesTemplateNodes.Count == 0)
                {
                    errorMes = string.Format("signals.xmlに{0}がない",callerClass);
                    isReSearch = true;
                    researchWord = baseClass;
                    return null;
                }
            }

            isReSearch = false;
            researchWord = "";
            errorMes = "";
            
            SignalTemplateData signalTemplateData1 = new SignalTemplateData();

            foreach (XmlNode node in typesTemplateNodes[0].ChildNodes)
            {
                if (node.Name == "method")
                {
                    signalTemplateData1.Method = node.InnerText;
                }else if (node.Name == "args")
                {
                    foreach (XmlNode argsNode in node.ChildNodes)
                    {
                        signalTemplateData1.ArgsArray.Add(argsNode.InnerText);
                    }
                }
            }

            return signalTemplateData1;
        }
        
        
    }
}