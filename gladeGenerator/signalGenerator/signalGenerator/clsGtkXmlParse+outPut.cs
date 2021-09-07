using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Xml;

namespace signalGenerator
{
    public partial class clsGtkXmlParse
    {
  
        //gladeの方 eventName すべて小文字　accel-closures-changed　大文字の前は-と小文字になっている
        //docの方　eventName  最初大文字の組み合わせ　ーがない
            
        //gladeの方 class GtkCheckButton
        //docの方　class Name="Button" FullName="Gtk.Button"
            
        // 結論
        // eventName  最初大文字の組み合わせ　ーがない に合わせる
        // class Name="Button" FullName="Gtk.Button" に合わせる
        
        private void _outPut(string saveFilePath)
        {
            XmlDocument xDoc = new XmlDocument();

            XmlNode rootNode = clsXml._mkXmlNode(xDoc, "root", null);
            xDoc.InsertAfter(rootNode,xDoc.FirstChild);
            
            foreach (TypeModel TypeModel1 in TypeModelArray)
            {
                XmlNode typeNodes = clsXml._mkXmlNode(xDoc, "types", new Hashtable()
                {
                    {"callerClass",TypeModel1.UIClassName_caller},
                    {"baseClass",TypeModel1.BaseClass}
                });

                foreach (signalModel signalModel1 in TypeModel1.SignalModels)
                {
                    
                    XmlNode signalNode = clsXml._mkXmlNode(xDoc, "signal", new Hashtable()
                    {
                        {"eventName",signalModel1.AttributeEventName},              
                        {"callerClass",signalModel1.UIClassName_caller},
                        {"baseClass",signalModel1.BaseClass}       
                    });

                    XmlNode methodN = clsXml._mkXmlNode(xDoc, "method",null);
                    signalNode.InsertAfter(methodN,signalNode.LastChild);
                    methodN.InnerText = signalModel1.AttributeEventName;    
                
                    XmlNode argsN = clsXml._mkXmlNode(xDoc, "args",null);
                    signalNode.InsertAfter(argsN,signalNode.LastChild);

                    foreach (string arg in signalModel1.Args)
                    {
                        XmlNode argN = clsXml._mkXmlNode(xDoc, "arg",null);
                        argsN.InsertAfter(argN,argsN.LastChild);
                        argN.InnerText = arg;
                    }
                    typeNodes.InsertAfter(signalNode,typeNodes.LastChild); 
                }
                
                rootNode.InsertAfter(typeNodes,rootNode.LastChild); 
 
            } 
            
            xDoc.Save(saveFilePath);
            
        }

    }
}