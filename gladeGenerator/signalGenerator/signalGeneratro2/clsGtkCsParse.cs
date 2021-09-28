using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml;

namespace signalGenerator2
{
    public class signalModel
    {
        public List<string> Args = new List<string>();
        public string AttributeEventName = "";
        public string UIClassName_caller = "";
        public string BaseClass = "";
    }
    public class TypeModel
    {
        public List<signalModel> SignalModels = new List<signalModel>();
        public string UIClassName_caller = "";
        public string BaseClass = "";
    }

    public partial class clsGtkCsParse
    {

        private static clsGtkCsParse _singleInstance = null;

        public string GtkFolder = "../../../Gtk/";
        public string GdkFolder = "../../../Gdk/";
        private string GtkSignalName = "signalGtk.xml";
        private string GdkSignalName = "signalGdk.xml";

        public static clsGtkCsParse Instance()
        {
            if (_singleInstance == null) {
                _singleInstance = new clsGtkCsParse();
            }
            return _singleInstance;
        }

        public void _parseGtk()
        {
            _loadCs(GtkSignalName);

            string saveFilePath = clsFile._getExePath_replace(GtkSignalName);

            _outPut(saveFilePath);
        }
        
        public void _parseGdk()
        {
            _loadCs(GdkSignalName);

            string saveFilePath = clsFile._getExePath_replace(GdkSignalName);

            _outPut(saveFilePath);
        }     

        private void _loadCs(string prefix)
        {
            string GtkFolder = clsGtkCsParse.Instance().GtkFolder ;

            ArrayList csListArray = clsFile._getFileList(GtkFolder, "*.cs");

            if (csListArray.Count == 0)
            {
                Console.Write("csファイルがない");
                return;
            }

            List<string> classFileArray = new List<string>();
            foreach (string filePath in csListArray)
            {
                if (filePath._indexOf("Handler") != -1 || filePath._indexOf("Args") != -1)
                {

                }
                else
                {
                    classFileArray.Add(filePath);
                }
            }

            _parseGtkXML(classFileArray,prefix);
        }


        private List<TypeModel> TypeModelArray = null;
        private void _parseGtkXML(List<string>  classFileArray,string prefix)
        {

            TypeModelArray = new List<TypeModel>();
            
            foreach (string filePath in classFileArray)
            {

                string csContent = clsFile._load_static(filePath);
                
                // public class Button : Bin, IActionable, IWrapper, IActivatable

                string patarn0 = @"public class(.*?)\:(.*?)\{";
                ArrayList classArray = clsUtility._patarnMatch(csContent, patarn0, 
                    1, 2,RegexOptions.Singleline);

                if (classArray.Count != 2)
                {
                    Console.WriteLine("public class　例外 {0} {1}", clsPath._getFileName(filePath) , classArray.Count);
                    continue;
                }

                string callerClassName = classArray[0].ToString().TrimStart().TrimEnd();
                string BaseClass = classArray[1].ToString().TrimStart().TrimEnd();
                if (BaseClass._indexOf(",") != -1)
                {
                    ArrayList splitArray = BaseClass._split(",");
                    if (splitArray.Count > 0)
                    {
                        BaseClass = splitArray[0].ToString();
                    }
                } 
                TypeModel TypeModel1 = new TypeModel();

                TypeModel1.BaseClass = BaseClass;
                TypeModel1.UIClassName_caller = callerClassName;
                
                if (callerClassName == "Button")
                {
                    Console.WriteLine("button");
                }

                string patarn = @"\[Signal\(\"".*?add.*?base.AddSignalHandler\((.*?)\;";
                ArrayList signalArray = clsUtility._patarnMatch(csContent, patarn, 1, -1,RegexOptions.Singleline);
               
                List<signalModel> signalModelArray = new List<signalModel>();
               
                foreach (string signalStr in signalArray)
                {
                    signalModel signalModel1 = new signalModel();
                    signalModel1.BaseClass = BaseClass;
                    signalModel1.UIClassName_caller = callerClassName;

                    string patarn2 = @"\""(.*?)\""(.*?)";
                    ArrayList addSignalArray = clsUtility._patarnMatch(signalStr, patarn2, 1, 2,RegexOptions.Singleline);

                    if (addSignalArray.Count != 2)
                    {
                        Console.WriteLine("addSignalArray　例外 {0} {1}",signalStr , addSignalArray.Count);
                        continue;
                    }

                    signalModel1.AttributeEventName = _convertEventName(addSignalArray[0].ToString());
                    string argsStr = addSignalArray[1].ToString();

 

                    List<string> Args = new List<string>();
                    if (argsStr._indexOf("typeof") != -1)
                    {
                        // base.AddSignalHandler("format-entry-text", value, typeof(FormatEntryTextArgs));
                        string patarn3 = @"typeof\((.*?)\)\;";
                        ArrayList typeOfArray = clsUtility._patarnMatch(argsStr, patarn3, 1, -1,RegexOptions.Singleline);
                        if (typeOfArray.Count != 1)
                        {
                            Console.WriteLine("typeOfArray　例外 {0} {1}",argsStr , typeOfArray.Count);
                            continue;
                        }
                        Args.Add("object sender");
                        string args_new = prefix + typeOfArray[0].ToString().TrimEnd().TrimStart() + " e";
                        Args.Add(args_new);
                    } else
                    {
                        Args.Add("object sender");
                        Args.Add("EventArgs e");  
                    }

                    signalModel1.Args = Args;
                    signalModelArray.Add(signalModel1);
                }

                TypeModel1.SignalModels = signalModelArray;
                
                TypeModelArray.Add(TypeModel1);

            }
        }

    }
}