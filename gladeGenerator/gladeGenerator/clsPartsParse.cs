using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using gladeGenerator;

namespace gladeGenerator
{
    public partial class clsPartsParse
    {

        List<GladeData> gladeDataArray = null;
        public clsPartsParse()
        {
            clsSignalsData.Instance();
            clsCodeHint.Instance();
        }
        public void _parsePrjectFolder(string fileFolderPath)
        {

            ArrayList filesArray = new ArrayList();
            
            if (clsFile._isFile(fileFolderPath) && fileFolderPath._indexOf(".glade") != -1)
            {
                filesArray.Add(fileFolderPath);
 
            }else if (clsFolder._isFolder(fileFolderPath))
            {
                filesArray = clsFile._getFileList(fileFolderPath, ".glade", null, isAllDir: true);
            }

            if (filesArray.Count == 0)
            {
                Console.WriteLine("フォルダ:{0} 書き出すGladeファイルがありません", fileFolderPath);
                return;
            }

            _parsePrjectFolder_do(filesArray);

        }

        public void _parsePrjectFolder_do(ArrayList filesArray)
        {

            gladeDataArray = new List<GladeData>();

            foreach (string gladeFilePath in filesArray)
            {
                string fileName = clsPath._getFileName(gladeFilePath);

                if (clsArgsConfig.Instance().NoGaldeFileArray.IndexOf(fileName) == -1)
                {
                    GladeData gladeDataPart1 = _parseGladeFile(gladeFilePath);
                    if (gladeDataPart1 != null)
                    {
                        gladeDataArray.Add(gladeDataPart1);
                    }
                }
            }

            foreach (var gladeDataPart1 in gladeDataArray)
            {
                _saveTopLabelPart_value(gladeDataPart1);
        
                // output
                _saveTopLabelPart_function(gladeDataPart1);   
            }

        }

        private XmlDocument gladeXmlDoc;

        /// <summary>
        /// Gladeを捜査する Topレベルのパーツを取得する
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private GladeData _parseGladeFile(string filePath)
        {
            GladeData gladeDataPart1 = null;

            if (clsFile._isFile(filePath))
            {
                gladeDataPart1 = new GladeData();

                gladeXmlDoc = new XmlDocument();
                gladeXmlDoc.Load(filePath);
                if (gladeXmlDoc == null || gladeXmlDoc.ChildNodes.Count == 0) {
                    return null;
                }
                
                XmlNode rootNodes = gladeXmlDoc.DocumentElement;

                XmlNodeList topLevelPartNodesList = rootNodes.SelectNodes("./object");
                if (topLevelPartNodesList == null || topLevelPartNodesList.Count == 0) {
                    return null;
                }

                foreach (XmlNode topObjectNode in topLevelPartNodesList)
                {

                    _getTopPart_idMethod(topObjectNode , ref gladeDataPart1.TopLevelPartArray);
                    
                    if (gladeDataPart1.TopLevelPartArray.Count == 0)
                    {
                        continue;
                    }

                    TopLevelPart topLevelPart1 = gladeDataPart1.TopLevelPartArray[gladeDataPart1.TopLevelPartArray.Count - 1];

                    _setGladeOutPutName(ref gladeDataPart1, ref topLevelPart1, filePath, topObjectNode);

                }

            }

            return gladeDataPart1;
        }

        private void _setGladeOutPutName(ref GladeData gladeDataPart1, 
            ref TopLevelPart topLevelPart1,
            string filePath , XmlNode topObjectNode)
        {

            string OutPutPartsIdName = topObjectNode.Attributes["id"] != null ? topObjectNode.Attributes["id"].Value : "" ;
            string GladeFileNameNoExtension = clsPath._getFileNameNoExtension(filePath);
            string GladeFileName = clsPath._getFileName(filePath);

            topLevelPart1.OutPutClassName =  clsArgsConfig.Instance().GladeFileClassMapDic.ContainsKey(GladeFileName) ? clsArgsConfig.Instance().GladeFileClassMapDic[GladeFileName]
                : GladeFileNameNoExtension;

            gladeDataPart1.OutPutGladeName =  clsArgsConfig.Instance().GladeFileClassMapDic.ContainsKey(GladeFileName)
                ? clsArgsConfig.Instance().GladeFileClassMapDic[GladeFileName]
                : GladeFileNameNoExtension;

            if (OutPutPartsIdName != "")
            {
                topLevelPart1.OutPutFileName_function = gladeDataPart1.OutPutGladeName + "+" + OutPutPartsIdName + ".cs";
                topLevelPart1.OutPutFileName_value = gladeDataPart1.OutPutGladeName + "+"　+ OutPutPartsIdName  + "_id" + ".cs"; 

                topLevelPart1.OutPutSaveFilePath_function = clsArgsConfig.Instance().SaveDir._trimEnd("/") + "/" + clsArgsConfig.Instance().ProjectName._trimEnd("/") + "/" + clsArgsConfig.Instance().AddSaveFolder.Trim('/') + "/" + topLevelPart1.OutPutFileName_function;
                topLevelPart1.OutPutSaveFilePath_value = clsArgsConfig.Instance().SaveDir._trimEnd("/") + "/" + clsArgsConfig.Instance().ProjectName._trimEnd("/") + "/" + clsArgsConfig.Instance().AddSaveFolder.Trim('/') + "/" + topLevelPart1.OutPutFileName_value;   
            }
          
        }

    }
}