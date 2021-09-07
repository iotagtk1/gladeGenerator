using System;
using System.Collections.Generic;

namespace gladeGenerator
{
    
    public class BaseClass : Object
    {
        public string DeclareValue; 
        public string ClassName;
        public string PartId;
        public List<Signal> SignalArray = new List<Signal>();
    } 
    
    public class ChildLevelPart : BaseClass
    {

    }

    public class TopLevelPart : BaseClass
    {
        public List<ChildLevelPart> ChildLevelPartsArray = new List<ChildLevelPart>();
        public string OutPutClassName;
        public string OutPutFileName_function;
        public string OutPutFileName_value;   
        public string OutPutSaveFilePath_function;  
        public string OutPutSaveFilePath_value;
    }

    public class GladeData : Object
    {
        public string GladeName;
        public string OutPutGladeName;
        public List<TopLevelPart> TopLevelPartArray = new List<TopLevelPart>();
    }
    public class Signal
    {
        public string ParentNodeClassName;
        public string BaseClass; 
        public string EventName;
        public string HandlerName;
        public string TargetObject;
        public Boolean IsAfter = false;
        public Boolean IsSwapped = false;
        public string OutPutMethodStr;
        public string ArgsStr;
        public string OutPutMethod_ArgsStr;  
    }

    public class SignalTemplateData
    {
       // public string ProtoTypeStr;  
        public string Method;
        public List<string> ArgsArray = new List<string>();
    }

}