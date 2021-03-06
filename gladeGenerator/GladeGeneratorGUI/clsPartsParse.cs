using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

using GladeGeneratorGUI;
using GLib;

namespace GladeGeneratorGUI
{
    public partial class clsPartsParse
    {
        List<GladeData> gladeDataArray = null;
        private MainWindow MainWindow1 = null;

        public clsPartsParse(MainWindow mainWindow1)
        {
            MainWindow1 = mainWindow1;
            
            clsSignalsData.Instance();
            clsCodeHint.Instance();
        }

        public void _parsePrjectFolder(string fileFolderPath)
        {
            try
            {

                ArrayList filesArray = new ArrayList();

                if (clsFile._isFile(fileFolderPath) && fileFolderPath._indexOf(".glade") != -1)
                {
                    filesArray.Add(fileFolderPath);
                }

                if (filesArray.Count == 0)
                {
                    Console.WriteLine("folder :{0} There is no Glade file to export", fileFolderPath);
                    return;
                }

                _parsePrjectFolder_do(filesArray);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public void _parsePrjectFolder_do(ArrayList filesArray)
        {
            gladeDataArray = new List<GladeData>();

            foreach (string gladeFilePath in filesArray)
            {
                string fileName = clsPath._getFileName(gladeFilePath);

                GladeData gladeDataPart1 = _parseGladeFile(gladeFilePath);
                if (gladeDataPart1 != null)
                {
                    gladeDataArray.Add(gladeDataPart1);
                }
            }

            if (gladeDataArray.Count > 0)
            {
                MainWindow1._mkTreeViewBinding_TopLevelPart(gladeDataArray[0]);
            }

        }

        /// <summary>
        /// 書き出し処理
        /// </summary>
        public void _outPut()
        {
            if (gladeDataArray != null)
            {
                //書き出す処理
                foreach (var gladeDataPart1 in gladeDataArray)
                {
                    _saveTopLabelPart_value(gladeDataPart1);

                    // output
                    _saveTopLabelPart_function(gladeDataPart1);
                }
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
                if (gladeXmlDoc == null || gladeXmlDoc.ChildNodes.Count == 0)
                {
                    return null;
                }

                XmlNode rootNodes = gladeXmlDoc.DocumentElement;

                XmlNodeList topLevelPartNodesList = rootNodes.SelectNodes("./object");
                if (topLevelPartNodesList == null || topLevelPartNodesList.Count == 0)
                {
                    return null;
                }

                foreach (XmlNode topObjectNode in topLevelPartNodesList)
                {
                    _getTopPart_idMethod(topObjectNode, ref gladeDataPart1.TopLevelPartArray);

                    if (gladeDataPart1.TopLevelPartArray.Count == 0)
                    {
                        continue;
                    }
                    
                    TopLevelPart topLevelPart1 =
                        gladeDataPart1.TopLevelPartArray[gladeDataPart1.TopLevelPartArray.Count - 1];

                    _setGladeOutPutName(ref gladeDataPart1, ref topLevelPart1, filePath, topObjectNode);
                }
            }

            return gladeDataPart1;
        }

        private void _setGladeOutPutName(ref GladeData gladeDataPart1,
            ref TopLevelPart topLevelPart1,
            string filePath, XmlNode topObjectNode)
        {
            string OutPutPartsIdName =
                topObjectNode.Attributes["id"] != null ? topObjectNode.Attributes["id"].Value : "";
            string GladeFileNameNoExtension = clsPath._getFileNameNoExtension(filePath);
            string GladeFileName = clsPath._getFileName(filePath);

            topLevelPart1.OutPutClassName = GladeFileNameNoExtension;

            gladeDataPart1.OutPutGladeName = GladeFileNameNoExtension;

            if (OutPutPartsIdName != "")
            {
                topLevelPart1.OutPutFileName_function =
                    gladeDataPart1.OutPutGladeName + "+" + OutPutPartsIdName + ".cs";
                topLevelPart1.OutPutFileName_value =
                    gladeDataPart1.OutPutGladeName + "+" + OutPutPartsIdName + "_id" + ".cs";

                topLevelPart1.OutPutSaveFilePath_function = clsArgsConfig.Instance().SaveDir._trimEnd("/") + "/" +
                                                            topLevelPart1.OutPutFileName_function;
                topLevelPart1.OutPutSaveFilePath_value = clsArgsConfig.Instance().SaveDir._trimEnd("/") + "/" +
                                                         topLevelPart1.OutPutFileName_value;
            }
        }
    }
}