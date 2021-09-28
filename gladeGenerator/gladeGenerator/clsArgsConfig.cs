using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;

namespace gladeGenerator
{
    public class clsArgsConfig
    {
        private static clsArgsConfig _singleInstance = null;

        public string SaveDir = "";     
        public string ProjectName = "";
        public string ProjectPath = "";
        public string FileDirPath = "";
        public string AddSaveFolder = "";
        public Boolean isCodeHint = false;
        public string codeHitFolder = "";
        public List<string> NoGaldeFileArray = new List<string>();
        public Dictionary<string,string> GladeFileClassMapDic = new Dictionary<string,string>();
        
        public static clsArgsConfig Instance()
        {
            if (_singleInstance == null) {
                _singleInstance = new clsArgsConfig();
            }
            return _singleInstance;
        }
        
        private clsArgsConfig()
        {
            try
            {            
                string ConfigSettingPath = clsFile._getExePath_replace("ConfigSetting.xml");

                XmlDocument configXmlDoc = new XmlDocument();
                configXmlDoc.Load(ConfigSettingPath);
                if (configXmlDoc != null)
                {
                    foreach (XmlNode node in configXmlDoc.FirstChild.ChildNodes)
                    {
                        if (node.Name == "Setting" && node.Attributes != null 
                        )
                        {
                            AddSaveFolder = node.Attributes._getValue("AddSaveFolder");
                            isCodeHint = node.Attributes._getValue("isCodeHint")._boolValue();
                            codeHitFolder = node.Attributes._getValue("codeHitFolder");
                        }
                    }
                }
                
                string GladeFileClassMapPath = clsFile._getExePath_replace("GladeFileClassMap.xml");

                XmlDocument gladeXmlDoc = new XmlDocument();
                gladeXmlDoc.Load(GladeFileClassMapPath);
                if (gladeXmlDoc != null)
                {
                    foreach (XmlNode node in gladeXmlDoc.FirstChild.ChildNodes)
                    {
                        if (node.Name == "gladFile" && node.Attributes != null 
                            )
                        {
                            //<gladFile targetFileName="" reNameClassName="" />
                            var key = node.Attributes._getValue("targetFileName");
                            var value = node.Attributes._getValue("reNameClassName");
                            if (key != "" && value != "")
                            {                                
                                GladeFileClassMapDic.Add(key,value);
                            }
                        }
                    }
                }
                
                string NgImportGladeFilePath = clsFile._getExePath_replace("NoImportGladeFileSetting.xml");

                XmlDocument NoImportGladeXmlDoc = new XmlDocument();
                NoImportGladeXmlDoc.Load(NgImportGladeFilePath);
                if (NoImportGladeXmlDoc != null)
                {
                    foreach (XmlNode node in NoImportGladeXmlDoc.FirstChild.ChildNodes)
                    {
                        if (node.Name == "gladeFile" && 
                            node.Attributes._getValue("targetFileName") != "")
                        {
                            // <gladFile targetFileName="" />
                            if (node.Attributes != null)
                            {
                                NoGaldeFileArray.Add(node.Attributes._getValue("targetFileName"));
                            }
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

        private List<string> commndKeyArray = new List<string> {
            "-projectPath","-projectName","-projectDir","-saveDir"};

        /// <summary>
        /// 引数が取得できているか
        /// </summary>
        /// <returns></returns>
        public Boolean _validateCommandKey()
        {
            if (ProjectPath != "")
            {
                ProjectName = _getProjectName(ProjectPath);
            }
            
            if (SaveDir == "" )
            {
                Console.WriteLine("-projectPathが指定されていない");
                return false;
            }
            if (ProjectName == "")
            {
                Console.WriteLine("-ProjectNameが指定されていない");
                return false;
            }
            if (ProjectPath == "")
            {
                Console.WriteLine("-projectPathが指定されていない");
                return false;
            }
            if (FileDirPath == "")
            {
                Console.WriteLine("-FileDirが指定されていない");
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
                        commndKeyArray.IndexOf(args[i+1]) == -1 && 
                        args[i+1] != ""){
                        FileDirPath = args[i + 1];
                    }
                    i++;
                    continue;
                } 
                
                if (commandKey._indexOf("-saveDir") != -1)
                {
                    if (args._safeIndexOf(i + 1) && 
                        commndKeyArray.IndexOf(args[i+1]) == -1 && 
                        args[i+1] != ""){
                        SaveDir = args[i + 1];
                    }
                    i++;
                    continue;
                }
                
                if (commandKey._indexOf("-projectName") != -1)
                {
                    
                    if (args._safeIndexOf(i + 1) && 
                        commndKeyArray.IndexOf(args[i+1]) == -1 && 
                        args[i+1] != ""){
                        ProjectName = args[i + 1];              
                    }
                    i++;
                    continue;
                }
                
                if (commandKey._indexOf("-projectPath") != -1)
                {
                    
                    if (args._safeIndexOf(i + 1) && 
                        commndKeyArray.IndexOf(args[i+1]) == -1 && 
                        args[i+1] != ""){
                        ProjectPath = args[i + 1];              
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
        private string _getProjectName(string path){
            
            path = path.TrimEnd(Path.DirectorySeparatorChar);

            var separator = Path.DirectorySeparatorChar;
            string[] pathArray = path.Split(separator);

            for (int i = pathArray.Length ; i > 0; i--)
            {
                var pathArray1 = pathArray[0..i];
                string stCsvData = string.Join("/", pathArray1);
                string csprojPath = stCsvData + "/" + pathArray[i-1] + ".csproj";
                if (File.Exists(csprojPath))
                {
                    return pathArray[i-1];
                    break;
                }
            }

            if (pathArray.Length == 0)
            {
                return path;
            }

            return "";
        }
        
  
       
    }
}