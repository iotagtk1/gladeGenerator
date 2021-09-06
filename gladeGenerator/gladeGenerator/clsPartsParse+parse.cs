using System;
using System.Collections.Generic;
using System.Xml;
using gladeGenerator;

namespace gladeGenerator
{
    /*
    * gladeに配置したコントロールからIDとメッソド名を抜きだし、メソッド文字列を作りモデルの中に入れる
    */
    public partial class clsPartsParse
    {
        private List<string> NoClassNameArray = new List<string>() {"Window", "Widget", "Dialog"};

        private void _getTopPart_idMethod(
            XmlNode topObjectNodes , 
            ref List<TopLevelPart> topLevelPartArray){

            if (topObjectNodes.Attributes["class"] != null && topObjectNodes.Attributes["id"] != null)
            {

                TopLevelPart topLevelPart1 = new TopLevelPart();

                topLevelPart1.PartId =
                    topObjectNodes.Attributes["id"] != null ? topObjectNodes.Attributes["id"].Value : "";
                topLevelPart1.ClassName =
                    topObjectNodes.Attributes["class"] != null ? topObjectNodes.Attributes["class"].Value : "";

                Boolean isNoClass = _isNoClassName(topObjectNodes);
                string addComment = "";
                if (isNoClass)
                {
                    addComment = "//";
                }        

                topLevelPart1.DeclareValue =
                    addComment + "[UI] private readonly " + topLevelPart1.ClassName._replaceReplaceStr("Gtk", "Gtk.") + " " +
                    topLevelPart1.PartId + " = null;";

                object TopLevelPart1_obj = topLevelPart1;
                _getSignals(topLevelPart1,topObjectNodes, ref TopLevelPart1_obj);
                topLevelPart1 = (TopLevelPart)TopLevelPart1_obj;

                XmlNodeList childLevelPartNodesList = topObjectNodes.SelectNodes(".//object");
                if (childLevelPartNodesList == null || childLevelPartNodesList.Count == 0)
                {
                    return;
                }
              
                _getChildPart_idMethod(topLevelPart1,childLevelPartNodesList, ref topLevelPart1.ChildLevelPartsArray);
                
                topLevelPartArray.Add(topLevelPart1);
  
            }
            else
            {
                
                Console.WriteLine("{0}にIDがないため書き出せません",topObjectNodes.Attributes["class"].Value);
            }
        }
        
        private void _getChildPart_idMethod(
            TopLevelPart TopLevelPart1,
            XmlNodeList childObjectNodes2 ,
            ref List<ChildLevelPart> childLevelPartArray ) {

            foreach (XmlNode childObjectNode in childObjectNodes2)
            {
                if (childObjectNode.Attributes["class"] == null || childObjectNode.Attributes["id"] == null)
                {
                    continue;
                }

                if (_isNoClassName(childObjectNode))
                {
                    continue;
                }
                
                ChildLevelPart childLevelPart2 = new ChildLevelPart();

                childLevelPart2.PartId = childObjectNode.Attributes["id"] != null ? childObjectNode.Attributes["id"].Value : "";
                childLevelPart2.ClassName =
                    childObjectNode.Attributes["class"] != null ? childObjectNode.Attributes["class"].Value : "";

                childLevelPart2.DeclareValue =
                    "[UI] private readonly " + childLevelPart2.ClassName._replaceReplaceStr("Gtk", "Gtk.") + " " + 
                    childLevelPart2.PartId + " = null;";
                
                object childLevelPart2_obj = childLevelPart2;
        
                _getSignals(TopLevelPart1,childObjectNode,ref childLevelPart2_obj);
        
                childLevelPartArray.Add(childLevelPart2);
                
            }
        
        }

        private void _getSignals(TopLevelPart topLevelPart1, XmlNode objectPartsNodes , ref object BaseLevelPart_a){
            try
            {
                BaseClass BaseLevelPart = (BaseClass)BaseLevelPart_a;
                
                XmlNodeList signalNodesList = objectPartsNodes.SelectNodes("./signal");

                if (signalNodesList == null || signalNodesList.Count == 0)
                {
                    //Console.WriteLine("ID:{0} Class:{1}のsignalの数が0",BaseLevelPart.PartId,BaseLevelPart.ClassName);
                    return;
                }
                else
                {
                    //Console.WriteLine("ID:{0} Class:{1}のsignalの数が{2}",BaseLevelPart.PartId,BaseLevelPart.ClassName, signalNodesList.Count);
                }
                
                foreach (XmlNode signalNodes in signalNodesList)
                {
                    
                    if (signalNodes.Attributes["handler"] == null || signalNodes.Attributes["name"] == null)
                    {
                        continue;
                    }
                    var classId = objectPartsNodes.Attributes["id"] != null ? objectPartsNodes.Attributes["id"].Value : "";

                    Signal signal = new Signal();

                    signal.ParentNodeClassName = objectPartsNodes.Attributes["class"] != null ? objectPartsNodes.Attributes["class"].Value : "";
                    signal.EventName = signalNodes.Attributes["name"] != null ? signalNodes.Attributes["name"].Value : "";

                    if (classId == "")
                    {
                        Console.WriteLine("Signal {0}はIDがないため書き出せません。IDを記入してください",signal.ParentNodeClassName);
                        continue;
                    }
                    
                    //EventNameをsignals.xmlの形式に合わせる
                    signal.EventName = _convertEventName(signal.EventName);

                    SignalTemplateData　signalTemplateData1 = clsSignalsData.Instance()._searchSignalTemplateData(
                        signal.ParentNodeClassName, 
                        signal.EventName);

                    if (signalTemplateData1 == null ){
                        Console.WriteLine("Signal {0}のEventがテンプレートデータ(signals.xml)にない。テンプレートデータ(signals.xml)を確認するか、テンプレートデータ(signal)プロジェクトを開いて更新してください",signal.ParentNodeClassName);
                        continue;
                    }
                    
                    signal.HandlerName = signalNodes.Attributes["handler"] != null ? signalNodes.Attributes["handler"].Value : "";                 
                    signal.TargetObject = signalNodes.Attributes["object"] != null ? signalNodes.Attributes["object"].Value : "";

                    if (signalNodes.Attributes["after"] != null)
                    {
                        signalNodes.Attributes["after"].Value = signalNodes.Attributes["after"].Value == "yes" ? "true" : "false";
                        signal.IsAfter = Boolean.Parse(signalNodes.Attributes["after"].Value);     
                    }

                    if (signalNodes.Attributes["swapped"] != null)
                    {
                        signalNodes.Attributes["swapped"].Value = signalNodes.Attributes["swapped"].Value == "yes" ? "true" : "false";
                        signal.IsSwapped = Boolean.Parse(signalNodes.Attributes["swapped"].Value); 
                    }

                    string topWinName = "";
                    if (clsArgsConfig.Instance().isWinNameHandlerInclude)
                    {
                        topWinName = topLevelPart1.PartId + "_";
                    }

                    signal.OutPutMethodStr = signal.HandlerName;

                    if (signalTemplateData1.ArgsArray.Count > 0)
                    {
                        foreach (var argStr in signalTemplateData1.ArgsArray)
                        {
                            signal.ArgsStr += argStr + " , ";
                        }
                        signal.ArgsStr = signal.ArgsStr._trimEnd(" , ");
                    }
                    
                    signal.OutPutMethod_ArgsStr = "private void " + signal.OutPutMethodStr + "("+ signal.ArgsStr + "){";

                    ((BaseClass)BaseLevelPart).SignalArray.Add(signal);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }
    }
}