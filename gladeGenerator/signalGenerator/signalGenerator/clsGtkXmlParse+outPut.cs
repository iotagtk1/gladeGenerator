using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Xml;

namespace signalGenerator
{
    public partial class clsGtkXmlParse
    {
        /*
          *callerClass　gtkを取る　小文字
          * 
           <signal eventName="Activate" callerClass="GtkButton">                           
             <prototype>bool on_activate_link(const std::string&amp; uri)</prototype>
             <method>Activate</method>
             <args>
               <arg>object sender</arg>
               <arg>EventArgs e</arg>
             </args>
           </signal> 
         */

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

            XmlNode signalsNode = clsXml._mkXmlNode(xDoc, "signals", null);
            xDoc.InsertAfter(signalsNode,xDoc.FirstChild);
  
            foreach (signalModel signalModel1 in signalModelArray)
            {
                XmlNode signalN = clsXml._mkXmlNode(xDoc, "signal", new Hashtable()
                {
                    {"eventName", signalModel1.AttributeEventName},{"callerClass",signalModel1.UIClassName_caller}
                });
                signalsNode.InsertAfter(signalN,signalsNode.LastChild);  
                
                XmlNode prototypeN = clsXml._mkXmlNode(xDoc, "prototype",null);
                signalN.InsertAfter(prototypeN,signalN.LastChild);
                prototypeN.InnerText = signalModel1.ProtoTypeMethod;
                
                XmlNode methodN = clsXml._mkXmlNode(xDoc, "method",null);
                signalN.InsertAfter(methodN,signalN.LastChild);
                methodN.InnerText = signalModel1.AttributeEventName;    
                
                XmlNode argsN = clsXml._mkXmlNode(xDoc, "args",null);
                signalN.InsertAfter(argsN,signalN.LastChild);

                foreach (string arg in signalModel1.Args)
                {
                    XmlNode argN = clsXml._mkXmlNode(xDoc, "arg",null);
                    argsN.InsertAfter(argN,argsN.LastChild);
                    argN.InnerText = arg;
                }
            }
            
            xDoc.Save(saveFilePath);
            
        }

    }
}