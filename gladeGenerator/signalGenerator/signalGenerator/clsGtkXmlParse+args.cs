using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Xml;

namespace signalGenerator
{
    public partial class clsGtkXmlParse
    {
        private string _loadEventHandlerFile(string returnTypeStr)
        {

            ArrayList returnTypeArray = returnTypeStr._split(".");
            if (returnTypeArray.Count > 1)
            {
                string folderName = returnTypeArray[0].ToString();
                string xmlName = returnTypeArray[1].ToString();

                string xmlFilePath = clsGtkXmlParse.Instance().GtkEnFolderPath + "/" + folderName + "/" + xmlName + ".xml";
                string xmlContent = clsFile._load_static(xmlFilePath);

                if (xmlContent != "")
                {
                    return xmlContent;
                }
                else
                {
                    Console.WriteLine("EventHandlerファイル{0}がありません",xmlFilePath );
                }
            }
            return "";
        }

        private List<string> _loadArgs(string xmlContent)
        {
            
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xmlContent);
            if (xDoc == null) {
                return null;
            }

            XmlNode typeNode = xDoc.SelectSingleNode("//Type");
            if (typeNode == null || typeNode.FirstChild == null) {
                return null;
            }
 
            XmlNode TypeNode = xDoc.SelectSingleNode("//Type/TypeSignature[@Language='C#']");
            if (TypeNode == null)
            {
                Console.WriteLine("");
                return null;
            }

            List<string> parameArray = new List<string>();
            XmlNodeList ParameterNodeList = xDoc.SelectNodes("//Type/*/Parameter");
            foreach (XmlNode parameterNode in ParameterNodeList)
            {
                if (parameterNode.Attributes["Name"].Value == "o")
                {
                    string o = parameterNode.Attributes["Type"].Value == "System.Object" ? "object sender" : parameterNode.Attributes["Type"].Value; 
                    parameArray.Add(o);
                }
                else if (parameterNode.Attributes["Name"].Value == "args"){
                    parameArray.Add(parameterNode.Attributes["Type"].Value +" e");
                }
            }

            if (parameArray.Count == 0)
            {
                Console.WriteLine(" 引数がない ");
            }
            
            return parameArray;
        }


    }
}