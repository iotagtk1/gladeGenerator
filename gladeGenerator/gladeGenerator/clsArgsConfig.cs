using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace gladeGenerator
{
    public class clsArgsConfig
    {
        private static clsArgsConfig _singleInstance = null;

        public string SaveDir = "";     
        public string ProjectName = "";
        public string FileDirPath = "";
        public string AddSaveFolder = "";
        public Boolean isWinNameHandlerInclude = false;
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
                            // <Setting isOverRideGladeEventHandler="0" AddSaveFolder="" />
                            //isOverRideGladeEventHandler = node.Attributes._getValue("isOverRideGladeEventHandler")
                            //    .ToString()._boolValue();
                            AddSaveFolder = node.Attributes._getValue("AddSaveFolder");  
                            
                            isWinNameHandlerInclude = Convert.ToBoolean(node.Attributes._getValue("isWinNameHandlerInclude").ToString());  
                            
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
            "-projectName","-projectDir","-saveDir"};
        public Boolean _validateCommandKey()
        {
            if (SaveDir == "" )
            {
                Console.WriteLine("projectFolderが指定されていない");
                return false;
            }
            if (ProjectName == "")
            {
                Console.WriteLine("ProjectNameが指定されていない");
                return false;
            }
            if (FileDirPath == "")
            {
                Console.WriteLine("FileDirが指定されていない");
                return false;
            }

            return true;
        }
        public void _setArgs(string[] args)
        {

            //-projectName $SolutionName$ -fileDir $FilePath$ -saveDir $SolutionDir$
            
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
             
                i++;
            }
        }
        
  
       
    }
}