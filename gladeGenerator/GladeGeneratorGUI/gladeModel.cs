using System;
using System.Collections.Generic ;

namespace GladeGeneratorGUI
{
    

    public class BaseClass : Object
    {
        public string DeclareValue { get; set; }
        public string ClassName { get; set; }
        public string PartId { get; set; }
        public List<Signal> SignalArray = new List<Signal>();
    } 
    
    public class ChildLevelPart : BaseClass
    {
     
    }

    public class TopLevelPart : BaseClass
    {
 
        
        public List<ChildLevelPart> ChildLevelPartsArray = new List<ChildLevelPart>();
        public string OutPutClassName { get; set; }
        public string OutPutFileName_function { get; set; }
        public string OutPutFileName_value { get; set; }  
        public string OutPutSaveFilePath_function { get; set; } 
        public string OutPutSaveFilePath_value { get; set; }
    }

    public class GladeData : Object
    {
        public string GladeName { get; set; }
        public string OutPutGladeName { get; set; }
        public List<TopLevelPart> TopLevelPartArray = new List<TopLevelPart>();
    }
    public class Signal
    {
        public string ParentNodeClassName { get; set; }
        public string BaseClass { get; set; }
        public string EventName { get; set; }
        public string HandlerName { get; set; }
        public string TargetObject { get; set; }
        public Boolean IsAfter = false;
        public Boolean IsSwapped = false;
        public string ArgsStr { get; set; }
        public string OutPutMethod_ArgsStr { get; set; }
        public string CodeHint { get; set; }
        public Boolean isReOutPut = true;
    }

    public class SignalTemplateData
    {
       // public string ProtoTypeStr { get; set; }  
        public string Method { get; set; }
        public List<string> ArgsArray = new List<string>();
    }

    
}