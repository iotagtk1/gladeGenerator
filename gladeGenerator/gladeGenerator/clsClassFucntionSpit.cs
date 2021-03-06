using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace gladeGenerator
{
    public class SplitData
    {
        public string FrontKey = "";
        public string FrontNkosuKey = "";    
        public string FrontStr = "";
        public string BackKey = ""; 
        public string BackNokosuKey = "";      
        public string BackStr = "";
        public string CenterStr = "";
        public Boolean IsTotalCenter = false;
    }
    public class clsClassFucntionSpit
    {
        public clsClassFucntionSpit(){}
        static public string _save_addMethod(string methonOnly, string addMethodStr,string splitStr)
        {

            if (splitStr == "")
            {
                return "";
            }

            SplitData splitData1 = new SplitData();
            splitData1.FrontKey = @"(^.*?namespace.*?\{)(.*?)";
            splitData1.FrontNkosuKey = @"$2";   
            splitData1.BackKey = @"(^.*?\})(.*?)";   
            splitData1.BackNokosuKey= @"$2";  
            
            clsClassFucntionSpit._splitStr(splitStr, ref splitData1);
            
            SplitData splitData2 = new SplitData();
            splitData2.FrontKey = @"(^.*?class.*?\{)(.*?)";
            splitData2.FrontNkosuKey = @"$2";   
            splitData2.BackKey = @"(^.*?\})(.*?)";
            splitData2.BackNokosuKey= @"$2";
            splitData2.IsTotalCenter = true;

            clsClassFucntionSpit._splitStr(splitData1.CenterStr, ref splitData2);

            if (splitData2.CenterStr._indexOf(methonOnly) == -1)
            { 
                splitData2.CenterStr += Environment.NewLine + "\t\t" + addMethodStr + Environment.NewLine + "\t\t\t" + Environment.NewLine + "\t\t}" ;
            }

            string sumTotalStr = clsClassFucntionSpit._margeStr(new List<SplitData>() {splitData1,splitData2});

            return sumTotalStr;
        }
        
        static string _margeStr(List<SplitData> splitDataArray)
        {
            string frontStr = "";
            string centerStr = "";           
            string backStr = "";

            int i = 0;
            foreach (SplitData splitData1  in splitDataArray)
            {
                if (splitData1.IsTotalCenter)
                {
                    centerStr += splitData1.CenterStr;
                }
                
                frontStr = frontStr + splitData1.FrontStr;
                
                backStr = splitData1.BackStr + backStr;
               
                i++;
            }
            
            var sumStr = frontStr + centerStr + backStr;
            
            return sumStr;
        }
        
        static SplitData _splitStr(string str ,ref SplitData splitData1 )
        {
            Regex reg = new Regex(splitData1.FrontKey, RegexOptions.Singleline);
            Match match = reg.Match(str);
            if (match.Index != -1 && match.Value != "")
            {
                splitData1.FrontStr = match.Value;
                string str2 = reg.Replace(str, splitData1.FrontNkosuKey);

                string str2_reverse = new string(str2.Reverse().ToArray());

                Regex reg2 = new Regex(splitData1.BackKey, RegexOptions.Singleline);
                Match match2 = reg2.Match(str2_reverse);
                if (match2.Index != -1 && match2.Value != "")
                {
                    var str2_ReReverse = new string(match2.Value.Reverse().ToArray());

                    splitData1.BackStr = str2_ReReverse;

                    string str2_ReReverse_2 = reg2.Replace(str2_reverse, splitData1.BackNokosuKey);
                    splitData1.CenterStr = new string(str2_ReReverse_2.Reverse().ToArray());

                    return splitData1;
                }
            }
            return null;
        }

    }
}