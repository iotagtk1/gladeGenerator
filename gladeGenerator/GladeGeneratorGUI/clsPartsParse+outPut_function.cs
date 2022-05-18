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
        public void _saveTopLabelPart_function(GladeData gladeDataPart1)
        {
            string fileContent = "";

            foreach (TopLevelPart TopLevelPart1 in gladeDataPart1.TopLevelPartArray)
            {
                if (TopLevelPart1.isReOutPut == false)
                {
                    continue;
                }
                if (clsFile._isFile(TopLevelPart1.OutPutSaveFilePath_function))
                {
                    fileContent = clsFile._load_static(TopLevelPart1.OutPutSaveFilePath_function);
                    if (fileContent == "")
                    {
                        fileContent = clsFile._load_static(clsFile._getExePath_replace("templateData/template.txt"));
                        fileContent = fileContent._replaceReplaceStr("{$value}", "");
                        fileContent = fileContent._replaceReplaceStr("{$className}", TopLevelPart1.OutPutClassName);
                        fileContent =
                            fileContent._replaceReplaceStr("{$nameSpace}", clsArgsConfig.Instance().ProjectName);
                    }
                }
                else
                {
                    fileContent = clsFile._load_static(clsFile._getExePath_replace("templateData/template.txt"));
                    fileContent = fileContent._replaceReplaceStr("{$value}", "");
                    fileContent = fileContent._replaceReplaceStr("{$className}", TopLevelPart1.OutPutClassName);
                    fileContent = fileContent._replaceReplaceStr("{$nameSpace}", clsArgsConfig.Instance().ProjectName);
                }

                foreach (Signal signal1 in TopLevelPart1.SignalArray)
                {
                    if (signal1.isReOutPut == false)
                    {
                        continue;
                    }
                    
                    string resultStr = clsClassFucntionSpit._save_addMethod(signal1.HandlerName,
                        signal1.OutPutMethod_ArgsStr, fileContent);
                    if (resultStr != "")
                    {
                        fileContent = resultStr;
                    }
                }

                foreach (ChildLevelPart childLevelPart1 in TopLevelPart1.ChildLevelPartsArray)
                {
                    foreach (Signal signal1 in childLevelPart1.SignalArray)
                    {
                        if (signal1.isReOutPut == false)
                        {
                            continue;
                        }
                        string resultStr = clsClassFucntionSpit._save_addMethod(signal1.HandlerName,
                            signal1.OutPutMethod_ArgsStr, fileContent);
                        if (resultStr != "")
                        {
                            fileContent = resultStr;
                        }
                    }
                }
                
                fileContent = clsCodeHint.Instance()._replaceCodeHint(MainWindow1, fileContent);

                Console.WriteLine("I saved it to　{0}", TopLevelPart1.OutPutSaveFilePath_function);
                clsFile._saveFilePath(fileContent, TopLevelPart1.OutPutSaveFilePath_function);
            }
        }
    }
}