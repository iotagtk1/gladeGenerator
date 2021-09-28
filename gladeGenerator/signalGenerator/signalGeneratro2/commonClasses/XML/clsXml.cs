using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.Linq;
using System.Xml.Linq;

public partial class clsXml {


    /// <summary>
    /// string to stream
    /// </summary>
    static public MemoryStream _xmlToStream(string str) {
        byte[] byteArray = Encoding.UTF8.GetBytes(str);
        MemoryStream stream = new MemoryStream(byteArray);
        return stream;
    }

    /// <summary>
    /// to xml  t 生成されたdataTableを入れる
    /// </summary>
    static public string _loadToXML(DataTable dt) {

        using (StringWriter textWriter = new StringWriter()) {
            XmlSerializer serializer = new XmlSerializer(dt.GetType());
            serializer.Serialize(textWriter, dt);
            return textWriter.ToString();
        }
        
    }

    /// <summary>
    /// to DataTable t 生成されたdataTableを入れる
    /// </summary>
    static public DataTable _readXmlToDataTable2(ref DataTable dt, string contentsXml) {

        DataSet ds = new DataSet();

        StringReader stringR = new System.IO.StringReader(contentsXml);
        ds.ReadXml(stringR);

        return ds.Tables[0];
    }
    

    /// <summary>
    /// XMNode削除
    /// </summary>
    static public void _deleteNode(XmlDocument document, string xPath) {

        //"/root/metadata"
        XmlNodeList nodeList = document.SelectNodes(xPath);
        for(int i = nodeList.Count - 1; i >= 0; i--) {
          nodeList[i].ParentNode.RemoveChild(nodeList[i]);
        } 
    } 


    /// <summary>
    /// XMNode追加
    /// </summary>
    static public XmlNode _mkXmlNode(XmlDocument document, string nodeName, Hashtable attributesDic) {

      XmlNode newSub = document.CreateNode(XmlNodeType.Element, nodeName, null);

      if(attributesDic != null) {
        foreach(string key in attributesDic.Keys) {
          XmlAttribute xa = document.CreateAttribute(key);
          xa.Value = attributesDic[key].ToString();
          newSub.Attributes.Append(xa);
        }
      }

      return newSub;
    }  

    /// <summary>
    /// XML出力
    /// </summary>
    static public string _mkXmlStr(XmlDocument dxmlDoc) {
        return dxmlDoc.OuterXml;
    }

    /// <summary>
    /// パース
    /// </summary>
    static public XmlDocument _mkXmlDocument(string path) {
        XmlDocument document = new XmlDocument();
        // ファイルから読み込む
        document.Load(path);
        return document;
    }
    
    
    /// <summary>
    /// XML名前空間を削除する　 
    /// </summary>
    static public XElement _removeAllNamespaces(XElement e)
    {
        return new XElement(e.Name.LocalName,
            (from n in e.Nodes()
                select ((n is XElement) ? _removeAllNamespaces(n as XElement) : n)),
            (e.HasAttributes) ? 
                (from a in e.Attributes() 
                    where (!a.IsNamespaceDeclaration)  
                    select new XAttribute(a.Name.LocalName, a.Value)) : null);
    } 
    
    
}

