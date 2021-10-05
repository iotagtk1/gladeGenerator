using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;

namespace GladeGeneratorGUI
{
    public class clsArgsConfig
    {
        private static clsArgsConfig _singleInstance = null;

        public string SaveDir = "";
        public string ProjectName = "";
        public string FileDirPath = "";
        public Boolean isCodeHint = false;
        public string codeHitFolder = "";

        public static clsArgsConfig Instance()
        {
            if (_singleInstance == null)
            {
                _singleInstance = new clsArgsConfig();
            }

            return _singleInstance;
        }

        private clsArgsConfig()
        {
            try
            {
                string ConfigSettingPath = clsFile._getExePath_replace("ConfigSetting.xml");

                if (!clsFile._isFile(ConfigSettingPath))
                {
                    clsFile._saveFilePath("",ConfigSettingPath);
                }

                XmlDocument configXmlDoc = new XmlDocument();
                configXmlDoc.Load(ConfigSettingPath);
                if (configXmlDoc != null)
                {
                    foreach (XmlNode node in configXmlDoc.FirstChild.ChildNodes)
                    {
                        if (node.Name == "Setting" && node.Attributes != null
                        )
                        {
                            isCodeHint = node.Attributes._getValue("isCodeHint")._boolValue();
                            codeHitFolder = node.Attributes._getValue("codeHitFolder");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private List<string> commndKeyArray = new List<string>
        {
            "-projectPath", "-projectName", "-projectDir"
        };

        /// <summary>
        /// 引数が取得できているか
        /// </summary>
        /// <returns></returns>
        public Boolean _validateCommandKey()
        {

            if (ProjectName == "")
            {
                Console.WriteLine("-ProjectNameが指定されていません");
                return false;
            }
            
            if (ProjectName != "")
            {
                ProjectName = _getProjectName(ProjectName);
            }

            if (clsIniFile.singlton[saveWin.saveFileText1, saveWin.saveFilePath] != "")
            {
                SaveDir = clsIniFile.singlton[saveWin.saveFileText1, saveWin.saveFilePath];
            }

            if (SaveDir == "")
            {
                Console.WriteLine("SaveDirがありません。Toolから設定してください。");
                return false;
            }
            if (FileDirPath == "")
            {
                Console.WriteLine("-FileDirが指定されていません");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 引数をセットする
        /// </summary>
        /// <param name="args"></param>
        public void _setArgs(string[] args)
        {
            int i = 0;
            foreach (var commandKey in args)
            {
                if (commandKey._indexOf("-fileDir") != -1)
                {
                    if (args._safeIndexOf(i + 1) &&
                        commndKeyArray.IndexOf(args[i + 1]) == -1 &&
                        args[i + 1] != "")
                    {
                        FileDirPath = args[i + 1];
                    }

                    i++;
                    continue;
                }

                if (commandKey._indexOf("-projectName") != -1)
                {
                    if (args._safeIndexOf(i + 1) &&
                        commndKeyArray.IndexOf(args[i + 1]) == -1 &&
                        args[i + 1] != "")
                    {
                        ProjectName = args[i + 1];
                    }

                    i++;
                    continue;
                }

                i++;
            }
        }

        /// <summary>
        /// プロジェクトパスを取得する
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string _getProjectName(string path)
        {
            path = path.TrimEnd(Path.DirectorySeparatorChar);

            var separator = Path.DirectorySeparatorChar;
            string[] pathArray = path.Split(separator);
            
            if (pathArray.Length == 0)
            {
                return path;
            }   

            for (int i = pathArray.Length; i > 0; i--)
            {
                var pathArray1 = pathArray[0..i];
                string stCsvData = string.Join("/", pathArray1);
                string csprojPath = stCsvData + "/" + pathArray[i - 1] + ".csproj";
                if (File.Exists(csprojPath))
                {
                    return pathArray[i - 1];
                    break;
                }
            }

            return "";
        }
    }
}