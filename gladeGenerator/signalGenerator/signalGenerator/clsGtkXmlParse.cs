using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml;

namespace signalGenerator
{
    public class signalModel
    {
        public List<string> Args = new List<string>();
        //public string MemberEvent = "";
        public string AttributeEventName = "";
        public string UIClassName_caller = "";
        public string ProtoTypeMethod = "";
    }
    public partial class clsGtkXmlParse
    {

        List<string> classFileArray;

        private static clsGtkXmlParse _singleInstance = null;

        public string GtkFolderPath = "";
        public string GtkEnFolderPath = ""; 
        
        public static clsGtkXmlParse Instance()
        {
            if (_singleInstance == null) {
                _singleInstance = new clsGtkXmlParse();
            }
            return _singleInstance;
        }

        public void _parse()
        {
            
            _loadGtkDoc();

            string saveFilePath = clsFile._getExePath_replace("signals.xml");

            _outPut(saveFilePath);
        }

        private void _loadGtkDoc()
        {
            string absoluteFilePath = clsGtkXmlParse.Instance().GtkFolderPath ;

            ArrayList xmlListArray = clsFile._getFileList(absoluteFilePath, "*.xml");
            classFileArray = new List<string>();
            foreach (string filePath in xmlListArray)
            {
                if (filePath._indexOf("Handler") != -1 || filePath._indexOf("Args") != -1)
                {

                }
                else
                {
                    classFileArray.Add(filePath);
                }
            }

            _parseGtkXML();
        }

        private List<signalModel> signalModelArray;
        private void _parseGtkXML()
        {
            signalModelArray = new List<signalModel>();
            foreach (string filePath in classFileArray)
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(filePath);
                if (xDoc == null) {
                    continue;
                }
                
                XmlNode typeNode = xDoc.SelectSingleNode("//Type");
                if (typeNode == null) {
                    Console.WriteLine("//Typeがない");
                    continue;
                }
                
                XmlNodeList membersNodesList = xDoc.SelectNodes("//Type/Members/Member");

                foreach (XmlNode memberNode in membersNodesList)
                {

                        XmlNode MemberSignatureNode = memberNode.SelectSingleNode("MemberSignature[@Language='C#']");
                        if (MemberSignatureNode == null)
                        {
                            continue;
                        }

                       //<MemberType>Event</MemberType>
                        XmlNode memberTypeNode = memberNode.SelectSingleNode("MemberType").FirstChild;
                        if (memberTypeNode != null && memberTypeNode.InnerText == "Event")
                        {
                            signalModel signalModel1 = new signalModel();
                            
                            // <Attributes>
                            //     <Attribute>
                            //     <AttributeName>GLib.Signal("leave")</AttributeName>
                            XmlNode AttributeNameNode = memberNode.SelectSingleNode("Attributes/Attribute/AttributeName");
                            if (AttributeNameNode != null && AttributeNameNode.InnerText != "")
                            {
                                Regex regex = new Regex("(\"(.*?)\")", RegexOptions.Singleline);
                                Match ma = regex.Match(AttributeNameNode.InnerText);
                                if (ma.Length > 0 && ma.Groups.Count > 0)
                                {
                                    var oldEventName = ma.Groups[0].Value._replaceReplaceStr("\"","");
                                    signalModel1.AttributeEventName = _convertEventName(oldEventName);
                                }
                            }

                            signalModel1.UIClassName_caller = typeNode.Attributes["FullName"].Value._replaceReplaceStr(".","");

                            signalModel1.ProtoTypeMethod = "on_{$ID}_" + signalModel1.AttributeEventName;

                            XmlNode returnTypeNode = memberNode.SelectSingleNode("ReturnValue/ReturnType").FirstChild;
                            if (returnTypeNode == null )
                            {
                                Console.WriteLine("ReturnValue/ReturnTypeがない");
                                continue;
                            }
                            if (returnTypeNode.Value == "System.EventHandler")
                            {
                                signalModel1.Args = new List<string>(){"object sender","EventArgs e"};
                            }else if (returnTypeNode.InnerText._indexOf(".") != -1)
                            {
                                string xmlContent = _loadEventHandlerFile(returnTypeNode.InnerText);
                                List<string> argsArray = _loadArgs(xmlContent);
                                if (argsArray != null)
                                {
                                    signalModel1.Args = argsArray; 
                                }
                            }
                            else
                            {
                                Console.WriteLine(".がない{0}",returnTypeNode.Value);
                                continue;  
                            }
                            signalModelArray.Add(signalModel1);
                        }
                }

            }
        }


    }
}