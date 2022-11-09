using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml;

namespace GladeGeneratorGUI
{
    /*
    * gladeに配置したコントロールからIDとメッソド名を抜きだし、メソッド文字列を作りモデルの中に入れる
    */
    public partial class clsPartsParse
    {
        private List<string> NoClassNameArray = new List<string>() { "Window", "Widget", "Dialog" };
        private List<string> CommentOutClassArray = new List<string>() { "Window" };

        private void _getTopPart_idMethod(
            XmlNode topObjectNodes,
            ref List<TopLevelPart> topLevelPartArray)
        {
            if (topObjectNodes.Attributes["class"] != null && topObjectNodes.Attributes["id"] != null)
            {
                TopLevelPart topLevelPart1 = new TopLevelPart();

                topLevelPart1.PartId =
                    topObjectNodes.Attributes["id"] != null ? topObjectNodes.Attributes["id"].Value : "";
                topLevelPart1.ClassName =
                    topObjectNodes.Attributes["class"] != null ? topObjectNodes.Attributes["class"].Value : "";

                Boolean isNoClass = _isCommentOutClassName(topObjectNodes);
                string addComment = "";
                if (isNoClass)
                {
                    addComment = "//";
                }

                topLevelPart1.DeclareValue =
                    addComment + "[UI] private " + topLevelPart1.ClassName._replaceReplaceStr("Gtk", "Gtk.") +
                    " " +
                    topLevelPart1.PartId + " = null;";

                object TopLevelPart1_obj = topLevelPart1;

                _getSignals(topLevelPart1, topObjectNodes, ref TopLevelPart1_obj);

                topLevelPart1 = (TopLevelPart)TopLevelPart1_obj;

                XmlNodeList childLevelPartNodesList = topObjectNodes.SelectNodes(".//object");
                if (childLevelPartNodesList == null || childLevelPartNodesList.Count == 0)
                {
                }
                else
                {
                    _getChildPart_idMethod(
                        topLevelPart1, 
                        childLevelPartNodesList,
                        ref topLevelPart1.ChildLevelPartsArray);
                }

                topLevelPartArray.Add(topLevelPart1);
            }
            else
            {
                Console.WriteLine("Check out Glade. Cannot export because there is no ID in {0} ", topObjectNodes.Attributes["class"].Value);
            }
        }

        private void _getChildPart_idMethod(
            TopLevelPart TopLevelPart1,
            XmlNodeList childObjectNodes2,
            ref List<ChildLevelPart> childLevelPartArray)
        {
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

                childLevelPart2.PartId = childObjectNode.Attributes["id"] != null
                    ? childObjectNode.Attributes["id"].Value
                    : "";
                childLevelPart2.ClassName =
                    childObjectNode.Attributes["class"] != null ? childObjectNode.Attributes["class"].Value : "";
                
                childLevelPart2.DeclareValue =
                    "[UI] private " + childLevelPart2.ClassName._replaceReplaceStr("Gtk", "Gtk.") + " " +
                    childLevelPart2.PartId + " = null;";

                object childLevelPart2_obj = childLevelPart2;

                _getSignals(TopLevelPart1, childObjectNode, ref childLevelPart2_obj);

                childLevelPartArray.Add(childLevelPart2);
            }
        }

        /// <summary>
        /// Signalを検索し、取得する
        /// </summary>
        /// <param name="topLevelPart1"></param>
        /// <param name="objectPartsNodes"></param>
        /// <param name="BaseLevelPart_a"></param>
        private void _getSignals(TopLevelPart topLevelPart1, XmlNode objectPartsNodes, ref object BaseLevelPart_a)
        {
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

                    var classId = objectPartsNodes.Attributes["id"] != null
                        ? objectPartsNodes.Attributes["id"].Value
                        : "";

                    Signal signalModel1 = new Signal();

                    signalModel1.ParentNodeClassName = objectPartsNodes.Attributes["class"] != null
                        ? objectPartsNodes.Attributes["class"].Value
                        : "";
                    signalModel1.EventName = signalNodes.Attributes["name"] != null
                        ? signalNodes.Attributes["name"].Value
                        : "";

                    if (classId == "")
                    {
                        Console.WriteLine("　There is no ID　{0}, please add one" +
                                          "Please check the template data. ({1})　", signalModel1.ParentNodeClassName,
                            clsSignalsData.Instance().signalFileName);
                        continue;
                    }

                    //EventNameをsignals.xmlの形式に合わせる
                    signalModel1.EventName = _convertEventName(signalModel1.EventName);

                    SignalTemplateData TemplateData_signal = clsSignalsData.Instance()._searchSignalTemplateData(
                        signalModel1.ParentNodeClassName,
                        signalModel1.EventName);

                    signalModel1.isReOutPut = true;
                    signalModel1.CodeHint = clsCodeHint.Instance()
                        ._searchUIHintCode(signalModel1.ParentNodeClassName, signalModel1.EventName);

                    if (TemplateData_signal == null)
                    {
                        Boolean isReSearch = true;
                        string errorMes = "";
                        string reSearchClassName = signalModel1.ParentNodeClassName;

                        for (int i = 0; i < 10; i++)
                        {
                            if (isReSearch && reSearchClassName != "")
                            {
                                signalModel1.CodeHint = clsCodeHint.Instance()
                                    ._searchUIHintCode(reSearchClassName, signalModel1.EventName);

                                TemplateData_signal = clsSignalsData.Instance()._searchBaseClassSignalTemplateData(
                                    reSearchClassName,
                                    signalModel1.EventName, ref errorMes, ref isReSearch,
                                    ref reSearchClassName);
                            }
                        }

                        if (TemplateData_signal == null)
                        {
                            Console.WriteLine("{0}の Signal {1} Eventがテンプレートデータ({2})にない。" +
                                              "Please check the template data.　{3}　", signalModel1.ParentNodeClassName,
                                signalModel1.EventName, clsSignalsData.Instance().signalFileName,
                                clsSignalsData.Instance().signalFileName);
                            continue;
                        }
                    }

                    signalModel1.HandlerName = signalNodes.Attributes["handler"] != null
                        ? signalNodes.Attributes["handler"].Value
                        : "";
                    signalModel1.TargetObject = signalNodes.Attributes["object"] != null
                        ? signalNodes.Attributes["object"].Value
                        : "";

                    if (signalNodes.Attributes["after"] != null)
                    {
                        signalNodes.Attributes["after"].Value =
                            signalNodes.Attributes["after"].Value == "yes" ? "true" : "false";
                        signalModel1.IsAfter = Boolean.Parse(signalNodes.Attributes["after"].Value);
                    }

                    if (signalNodes.Attributes["swapped"] != null)
                    {
                        signalNodes.Attributes["swapped"].Value =
                            signalNodes.Attributes["swapped"].Value == "yes" ? "true" : "false";
                        signalModel1.IsSwapped = Boolean.Parse(signalNodes.Attributes["swapped"].Value);
                    }

                    if (TemplateData_signal.ArgsArray.Count > 0)
                    {
                        foreach (var argStr in TemplateData_signal.ArgsArray)
                        {
                            signalModel1.ArgsStr += argStr + " , ";
                        }

                        signalModel1.ArgsStr = signalModel1.ArgsStr._trimEnd(" , ");
                    }

                    signalModel1.OutPutMethod_ArgsStr =
                        "private void " + signalModel1.HandlerName + "(" + signalModel1.ArgsStr + "){";

                    if (signalModel1.CodeHint != "")
                    {
                        signalModel1.OutPutMethod_ArgsStr = signalModel1.OutPutMethod_ArgsStr + Environment.NewLine + signalModel1.CodeHint;
                    }

                    ((BaseClass)BaseLevelPart).SignalArray.Add(signalModel1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}