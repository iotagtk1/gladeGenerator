using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;


    public static partial class xmlExtensions {


    /// <summary>
    /// _addRootTag　 
    /// </summary>
    public static string _addRootTag(this string str ,string addTag = "root") {
        string a = "<{0}>"._stringWithFormat(addTag) + str + "</{0}>"._stringWithFormat(addTag);
        return a;
    }

    /// <summary>
    /// setXmlDocument　 
    /// </summary>
    public static XmlNamespaceManager _setNameSpace(this XmlDocument doc) {
        XmlNamespaceManager xmlNsManager = new XmlNamespaceManager(doc.NameTable);
        xmlNsManager._setNsNameSpace(doc);

        return xmlNsManager;
    }


    /// <summary>
    /// _addNodeElement　 
    /// </summary>
    public static void _addNodeElement(this XmlNode xmlEle, XmlElement xmlInsertData) {
        if (xmlEle == null) {
            Console.WriteLine("xmlEleがnull  " + xmlInsertData);
            return;
        }
        xmlEle.AppendChild(xmlEle.OwnerDocument.ImportNode(xmlInsertData, true));
    }

    /// <summary>
    /// _addNodeElement　 
    /// </summary>
    public static void _addNodeElement(this XmlElement xmlEle, XmlElement xmlImportNodeData) {
        xmlEle.AppendChild(xmlEle.OwnerDocument.ImportNode(xmlImportNodeData, true));
    }

    /// <summary>
    /// _afterNodeElement　 
    /// </summary>
    public static void _afterNodeElement(this XmlElement xmlEle, XmlElement xmlAfterNodeData) {
        xmlEle.InsertAfter(xmlAfterNodeData, xmlEle.LastChild);
    }

    /// <summary>
    /// _addNodeXml　 
    /// </summary>
    public static void _addNodeXml(this XmlNode xmlEle, string xmlStr) {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xmlStr);
        xmlEle.AppendChild(xmlEle.OwnerDocument.ImportNode(doc.DocumentElement, true));
    }


    /// <summary>
    /// _addNodeXml　 
    /// </summary>
    public static void _addNodeXml(this XmlElement xmlEle, string xmlStr) {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xmlStr);        
        xmlEle.AppendChild(xmlEle.OwnerDocument.ImportNode(doc.DocumentElement, true));
    }

    /// <summary>
    /// _addRootNodeElement　 
    /// </summary>
    public static void _addRootNodeElement(this XmlDocument xmlDoc) {
        XmlElement element1 = xmlDoc.CreateElement("root");
        xmlDoc.AppendChild(element1);
    }

    /// <summary>
    /// _addNodeElement　 
    /// </summary>
    public static void _addNodeElement(this XmlDocument xmlDoc,string nodeName) {

        XmlElement element1 = xmlDoc.CreateElement(nodeName);
        xmlDoc.AppendChild(element1);

    }

    /// <summary>
    /// _getXmlElementParts　 
    /// </summary>
    public static XmlDocumentFragment _getXmlElementParts_import(this XmlDocument xmlDoc, string xmlPath) {
        XmlDocumentFragment xmlDocFragment = xmlDoc.CreateDocumentFragment();
        xmlDocFragment.InnerXml = clsFile._load_static(xmlPath);
        return xmlDocFragment;
    }

    /// <summary>
    /// NsNameSpaceを追加　 
    /// </summary>
    /// XmlNodeList xNodeList2 = xmlRootDoc1.SelectNodes("ns:PropertyGroup", xmlNsManager);
    /// 
    public static void _setNsNameSpace(this XmlNamespaceManager xmlMane,XmlDocument doc) {
        xmlMane.AddNamespace("ns", doc.DocumentElement.NamespaceURI);
    }


    /// <summary>
    /// xmlの属性の一つを文字で取得する
    /// </summary>
    public static string _getAttributesValue(this XmlElement xmlElemnet, string key) {
        if (xmlElemnet.Attributes == null) {
            return "";
        }
        foreach (XmlAttribute a in xmlElemnet.Attributes) {
            if (a.Name == key) {
                return a.Value;
            }
        }
        return "";
    }

    /// <summary>
    /// xmlの属性の一つを文字で取得する
    /// </summary>
    public static string _getAttributesValue(this XmlNode xmlNode, string key) {
        if (xmlNode.Attributes == null) {
            return "";
        }
        foreach (XmlAttribute a in xmlNode.Attributes) {
            if (a.Name == key) {
                return a.Value;
            }
        }
        return "";
    }


    /// <summary>
    /// xmlelementを削除する
    /// </summary>
    public static void _remove(this XmlElement xmlElemnet) {
        xmlElemnet.ParentNode.RemoveChild(xmlElemnet);
    }
    /// <summary>
    /// XmlNodeを削除する
    /// </summary>
    public static void _remove(this XmlNode xmlElemnet) {
        xmlElemnet.ParentNode.RemoveChild(xmlElemnet);
    }


    /// <summary>
    /// xmlの属性を文字列に書き出す
    /// </summary>
    public static string _getAttributesStr(this XmlAttributeCollection xmlAttribute) {
        Hashtable array = new Hashtable();
        foreach (XmlAttribute a in xmlAttribute) {
            array.Add(a.Name,a.Value);   
        }     
        string jsonStr = array._toJson();

        return jsonStr;
    }

    /// <summary>
    /// xmlの属性の一つを文字で取得する
    /// </summary>
    public static string _getValue(this XmlAttributeCollection xmlAttribute, string key) {
        foreach (XmlAttribute a in xmlAttribute) {
            if (a.Name == key) {
                return a.Value;
            }
        }
        return "";
    }

    /// <summary>
    /// xmlの属性の一つを文字をセットする 
    /// 新規のAttr名ならXmlDocも追加すること
    /// </summary>
    public static void _setAttributesValue(this XmlElement xmlAttribute,  string key, string value,
        XmlDocument xmlDoc_sinkiAddYou = null) {
        xmlAttribute.Attributes._setAttributesValue(key, value, xmlDoc_sinkiAddYou);
    }
    /// <summary>
    /// xmlの属性の一つを文字をセットする
    /// 新規のAttr名ならXmlDocも追加すること
    /// </summary>
    public static void _setAttributesValue(this XmlNode xmlAttribute, string key, string value,
        XmlDocument xmlDoc_sinkiAddYou = null) {
        xmlAttribute.Attributes._setAttributesValue(key, value, xmlDoc_sinkiAddYou);
    }


    /// <summary>
    /// xmlの属性の一つを文字をセットする
    /// 新規のAttr名ならXmlDocも追加すること
    /// </summary>
    public static void _setAttributesValue(
        this XmlAttributeCollection xmlAttribute, 
        string key, string value, 
        XmlDocument xmlDoc_sinkiAddYou = null) {

        try {

            if (xmlAttribute == null) {
                return;
            }
            Boolean hitFlag = false;
            foreach (XmlAttribute a in xmlAttribute) {
                if (a.Name == key) {
                    a.Value = value;
                    hitFlag = true;
                    return;
                }
            }
            if (!hitFlag && xmlDoc_sinkiAddYou != null) {
                System.Xml.XmlAttribute Attr = xmlDoc_sinkiAddYou.CreateAttribute(key);
                Attr.Value = value;
                xmlAttribute.Append(Attr);
            }

        } catch (Exception en) {

        }


    }



    /// <summary>
    /// xmlの属性をjsonで全部取得
    /// </summary>
    public static string _getXmlAttributeStr(this XmlAttributeCollection xmlAttribute) {

        Hashtable XmlAttributeDic = new Hashtable();
        foreach (XmlAttribute a in xmlAttribute) {
            XmlAttributeDic.Add(a.Name, a.Value);
        }

        return XmlAttributeDic._toJson();
    }



    public static void _setValue(this XmlElement xmlElement, string key , string value ) {
        if (xmlElement.Attributes.Count > 0) {
            foreach (XmlAttribute attr in xmlElement.Attributes) {
                if (attr.Name == key) {
                    attr.Value = value;
                }
            }
        }
    }


    public static string _searchAttr(this XmlElement xmlElement,string searchAttrName) {

            if (xmlElement == null) {
                return "";
            }

            if (xmlElement.Attributes.Count > 0) {
                foreach (XmlAttribute attr in xmlElement.Attributes) {
                    if (attr.Name == searchAttrName) {                    
                        return attr.Value.ToString();
                    }
                }
            }
            return "";
        }

        public static string _escapeXml_C2A0(this string xml) {

           string xml1 = xml._replaceReplaceStr("\xc2\xa0", " ");

           return xml1;
        }

        public static string _escapeXml_1_0(this string xml) {

            var sb = new System.Text.StringBuilder();

                foreach (var c in xml) {
                    var code = (int)c;

                    if (code == 0x9 ||
                        code == 0xa ||
                        code == 0xd ||
                        (0x20 <= code && code <= 0xd7ff) ||
                        (0xe000 <= code && code <= 0xfffd) ||
                        (0x10000 <= code && code <= 0x10ffff)) {
                        sb.Append(c);
                    }
                }

                return sb.ToString();
        }

        public static XElement _toXElement(this XmlDocument doc)
        {
            XElement root = XElement.Parse(doc.OuterXml);
            return root;
        }

        public static string _getAttribute(this XElement e ,string name)
        {
            var attr = e.Attribute(name).ToString();
            return attr;
        } 
        
        public static string _getValu(this XElement e)
        {
            var val = e.Value.ToString();
            return val;
        } 
        
        public static T _getAttribute<T>(this XElement e, string name)
        {
            var attr = e.Attribute(name);
            if (attr == null)
                return default(T);
            else
                return (T)Convert.ChangeType(attr.Value, typeof(T));
        }


}









