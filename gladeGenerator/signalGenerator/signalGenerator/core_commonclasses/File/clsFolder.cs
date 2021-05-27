using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;

public partial class clsFolder {

    public clsFolder() {
    
    }

    /// <summary>
    /// dirからXMLを取得する
    /// </summary>
    static public string _getDir_to_XmlString(
        string filePath ,
        string filterStr,
        ArrayList ngDirFilterArray,
        ArrayList ngImportFilterArray

        ) {

        DirectoryInfo dir = new DirectoryInfo(filePath);

        XElement xmlInfo = new XElement("serverfiles");

        foreach (FileInfo file in dir.GetFiles(filterStr)) {
            if (ngImportFilterArray != null) {
                bool hitFlag = ngImportFilterArray._safeSearch_bool(file.Name);
                if (hitFlag) {
                    continue;
                }
            }

            xmlInfo.Add(new XElement("file",
                new XAttribute("NodeName", file.Name),
                new XAttribute("Path", file.DirectoryName + @"\" + file.Name)
                ));         

        }

        var subdirectories = dir.GetDirectories().ToList().OrderBy(d => d.Name);

        if (subdirectories.Count() == 0 && xmlInfo.Descendants("file").Count() == 0) {
            return null;
        }

        foreach (var subDir in subdirectories) {
            xmlInfo.Add(_createSubdirectoryXML(subDir, filterStr, ngDirFilterArray, ngImportFilterArray));
        }

        var reader = xmlInfo.CreateReader();
        reader.MoveToContent();

        return reader.ReadInnerXml();
    }

    private static XElement _createSubdirectoryXML(DirectoryInfo dir,
        string filterStr ,
        ArrayList ngDirFilterArray,
        ArrayList ngImportFilterArray

        ) {

        XElement xmlInfo = new XElement("folder", 
            new XAttribute("NodeName", dir.Name),
            new XAttribute("Path", dir.Name)
            );

        if (ngDirFilterArray != null) {
            bool hitFlag = ngDirFilterArray._searchValue(dir.Name);
            if (hitFlag) {
                return null;
            }
        }
 

        foreach (FileInfo file in dir.GetFiles(filterStr)) {

            if (ngImportFilterArray != null) {
                bool hitFlag = ngImportFilterArray._safeSearch_bool(file.Name);
                if (hitFlag) {
                    continue;
                }
            }

            xmlInfo.Add(new XElement("file",
                new XAttribute("NodeName", file.Name),
                new XAttribute("Path", file.DirectoryName + @"\" + file.Name)
                ));
        }

        var subdirectories = dir.GetDirectories().ToList().OrderBy(d => d.Name);

        if (subdirectories.Count() == 0 && xmlInfo.Descendants("file").Count() == 0) {
            return null;
        }

        foreach (var subDir in subdirectories) {
            XElement xele = _createSubdirectoryXML(subDir, filterStr, ngDirFilterArray, ngImportFilterArray);
            if (xele != null) {
                xmlInfo.Add(xele);
            }                      
        }

        return xmlInfo;
    }
    


 

  }

