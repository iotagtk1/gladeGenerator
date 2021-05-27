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

        public SignalTemplateData _searchSignalTemplateData(string callerClassName , string eventName)
        {
            string selectStr = "//signals/signal[@eventName='" + eventName + "' and @callerClass='" + callerClassName +
                               "']";
            
            XmlNodeList signalNodes = xmlDoc.SelectNodes(selectStr);

            if (signalNodes == null || signalNodes.Count == 0)
            {
                return null;
            }

            SignalTemplateData signalTemplateData1 = new SignalTemplateData();

            foreach (XmlNode node in signalNodes[0].ChildNodes)
            {

                if (node.Name == "prototype")
                {
                    signalTemplateData1.ProtoTypeStr = node.InnerText;
                }else if (node.Name == "method")
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