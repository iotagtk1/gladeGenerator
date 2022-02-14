using System;
using System.Collections.Generic;
using System.Xml;

namespace GladeGeneratorGUI
{
    /*
     *メッソド名をテンプレートから生成する
     */
    public partial class clsPartsParse
    {
        public void _saveTopLabelPart_value(GladeData GladeData1)
        {
            int i = 0;
            foreach (TopLevelPart topLevelPart1 in GladeData1.TopLevelPartArray)
            {
                if (topLevelPart1.isReOutPut == false)
                {
                    continue;
                }
                var fileContent = "";
                if (clsFile._isFile(topLevelPart1.OutPutSaveFilePath_value))
                {
                    fileContent = clsFile._load_static(topLevelPart1.OutPutSaveFilePath_value);
                    if (fileContent == "")
                    {
                        fileContent = clsFile._load_static(clsFile._getExePath_replace("templateData/template.txt"));
                        fileContent = fileContent._replaceReplaceStr("{$className}", topLevelPart1.OutPutClassName);
                        fileContent =
                            fileContent._replaceReplaceStr("{$nameSpace}", clsArgsConfig.Instance().ProjectName);
                    }
                }
                else
                {
                    fileContent = clsFile._load_static(clsFile._getExePath_replace("templateData/template.txt"));
                    fileContent = fileContent._replaceReplaceStr("{$className}", topLevelPart1.OutPutClassName);
                    fileContent = fileContent._replaceReplaceStr("{$nameSpace}", clsArgsConfig.Instance().ProjectName);
                }

                string resultStr = clsClassValueSpit._save_addMethod(topLevelPart1.DeclareValue, fileContent);
                if (resultStr != "")
                {
                    fileContent = resultStr;
                }

                foreach (ChildLevelPart childLevelPart1 in topLevelPart1.ChildLevelPartsArray)
                {
                    string resultStr2 =
                        clsClassValueSpit._save_addMethod(childLevelPart1.DeclareValue, fileContent);
                    if (resultStr2 != "")
                    {
                        fileContent = resultStr2;
                    }
                }

                if (i == GladeData1.TopLevelPartArray.Count - 1)
                {
                    fileContent = _kakoFix(fileContent);
                }

                i++;
                Console.WriteLine("I saved it to {0}", topLevelPart1.OutPutSaveFilePath_value);
                clsFile._saveFilePath(fileContent, topLevelPart1.OutPutSaveFilePath_value);
            }
        }
    }
}