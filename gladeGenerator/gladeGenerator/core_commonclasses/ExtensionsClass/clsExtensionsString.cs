using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Globalization;

    public static partial class StringExtensions {


    /// <summary>
    /// パスを数字で分けて取る 右から 0 1 2 3
    /// </summary>
    static public string _get_path_spltRightNum(this string path, int rightNum) {

        if (path._indexOf(@"\") != -1) {
            ArrayList array = path._split(@"\");
            if (array._safeIndexOf(array.Count - (rightNum+1))) {
                ArrayList newArray = array.GetRange(0, array.Count - (rightNum + 1));
                string newPath1 =  newArray._join(@"\");
                return newPath1;
            }
        }      

        return "";
    }
    

    /// <summary>
    /// ファイル名を変更する
    /// </summary>
    static public string _changeFileTempName_formPath(this string path, string addTempName = "temp") {
        string newPath = Path.GetDirectoryName(path) + @"\" + 
            Path.GetFileNameWithoutExtension(path) + 
            addTempName + Path.GetExtension(path);
        return newPath;
    }

    /// <summary>
    /// ファイルの指定した文字の行番号を取得する
    /// </summary>
    static public int _getLineNum(this string str ,string searchWord) {

        ArrayList splitArray = str._split(Environment.NewLine);

        int i = 1;
        foreach (string str2 in splitArray) {
            if (str2._indexOf(searchWord) != -1) {
                return i;
            }
            i++;
        }

        return 1;
    }

    /// <summary>
    /// ファイル名を取得する
    /// </summary>
    static public string _getFileNameNoExtension(this string str) {

        if (str._indexOf(".") != -1) {
            return str._split(".")[0].ToString();
        }

        return str;
    }

    /// <summary>
    /// 文字列を取得する
    /// </summary>
    static public string _getFirstStringLength(this string str,int lengthNum) {
        string s1 = str.Substring(0, lengthNum);
        return s1;
    }    

    /// <summary>
    /// 文字コードエンコード
    /// </summary>
    static public string _convertSJIS_To_UTF_8(this string str) {
        //まずはバイト配列に変換する
        byte[] bytesUTF8 = System.Text.Encoding.Default.GetBytes(str);

        //バイト配列をUTF8の文字コードとしてStringに変換する
        string stringSJIS = System.Text.Encoding.UTF8.GetString(bytesUTF8);

        return stringSJIS;
    }

    /// <summary>
    /// ファイルの作成された日時を取得する
    /// </summary>
    static public DateTime _getCreateDate(this string filePath) {
        FileInfo fInfo = new FileInfo(filePath);
        return fInfo.CreationTime;
    }

    /// <summary>
    /// ファイル情報を取得する
    /// </summary>
    static public FileInfo _getFileInfo(this string filePath) {
            FileInfo fInfo = new FileInfo(filePath);
            return fInfo;
        }    

        /// <summary>
        /// 指定した名前のあるフォルダまだでのパスを取得する
        /// </summary>
        static public string _getTargetPath(this string dirPath,string targetName) {

        var targetPath = "";
        ArrayList array = dirPath._split(@"\");          

        if (array.Count > 0) {
            foreach (string str in array) {

                targetPath += str + @"\"; 

                if (str._indexOf(targetName) != -1) {
                    char[] charsToTrim = { '\\' };
                    targetPath = targetPath.TrimEnd(charsToTrim);
                    break;
                }

            }
        }

        return targetPath;
    }
 

    /// <summary>
    /// string to stream
    /// </summary>
    static public MemoryStream _toStream(this string str) {
        byte[] byteArray = Encoding.UTF8.GetBytes(str);
        MemoryStream stream = new MemoryStream(byteArray);
        return stream;
    }

    static public XmlReader _toXmlReader(this string str) {

        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.LoadXml(str);

        MemoryStream xmlStream = new MemoryStream();
        xmlDoc.Save(xmlStream);

        xmlStream.Flush();//Adjust this if you want read your data 
        xmlStream.Position = 0;

        //  MemoryStream mStrm1 = new MemoryStream(Encoding.UTF8.GetBytes(str));
        // byte[] data = Encoding.UTF8.GetBytes(str);
        // MemoryStream stm = new MemoryStream(data, 0, data.Length);

        StringReader TheStream = new StringReader(str);

        XmlReader reader = XmlReader.Create(TheStream);
         return reader;
    }




    /// <summary>
    /// 半角全角英数
    /// </summary>
    static public Boolean _isEisuu(this string str) {

      if(System.Text.RegularExpressions.Regex.IsMatch(str,
            @"^[a-zA-Zａ-ｚＡ-Ｚ0-9]+$")) {
            Console.WriteLine("すべて 半角アルファベット です。");
            return true;
        }

        return false;
    }

    /// <summary>
    /// 漢字かどうか
    /// </summary>
    static public Boolean _isKanji(this string str) {

        Boolean b = Regex.IsMatch(str, @"^\p{IsCJKUnifiedIdeographs}*$");

        return b;
    }

    /// <summary>
    /// ひらがなかどうか
    /// </summary>
    static public Boolean _isHiragana(this string str) {

        Boolean b = Regex.IsMatch(str, @"^\p{IsHiragana}*$");

        return b;
    }

    /// <summary>
    /// カタカナかどうか
    /// </summary>
    static public Boolean _isKatakana(this string str) {

        Boolean b = Regex.IsMatch(str, @"^\p{IsKatakana}*$");

        return b;
    }

    /// <summary>
    /// 置き換え　() に対し$1　$2  $3 
    /// </summary>
    static public string _patarnReplace(this string str, string patarn,string replaceStr) {

        //連続する同じ行を削除
        var str2 = Regex.Replace(str, patarn, replaceStr, RegexOptions.IgnoreCase | RegexOptions.Multiline);
        return str2;
    }


    /// <summary>
    /// 置き換え　() に対し$1　$2  $3 
    /// </summary>
    static public string _patarnReplace_singleLine(this string str, string patarn, string replaceStr) {

        //連続する同じ行を削除
        var str2 = Regex.Replace(str, patarn, replaceStr, RegexOptions.IgnoreCase | RegexOptions.Singleline);
        return str2;
    }


    /// <summary>
    /// パターンマッチ
    /// </summary>
    static public ArrayList _patarnMatch(this string str, string patarn, int kakko = -1, int kakko2 = -1) {

        //正規表現パターンとオプションを指定してRegexオブジェクトを作成
        Regex r = new Regex(
                patarn, RegexOptions.IgnoreCase | RegexOptions.Multiline);

        //TextBox1.Text内で正規表現と一致する対象をすべて検索
        MatchCollection mc = r.Matches(str);

        ArrayList array = new ArrayList();

        foreach (Match m in mc) {
            //正規表現に一致したグループと位置を表示

            if (kakko != -1) {
                array.Add(m.Groups[kakko].Value.ToString());
            }
            if (kakko2 != -1) {
                array.Add(m.Groups[kakko2].Value.ToString());
            }
        }

        return array;
    }

    /// <summary>
    /// booleanにする
    /// </summary>
    public static Boolean _boolValue(this string str) {
        if (str == "" || str == null) {
            str = "false";
        }
        return Convert.ToBoolean(str);
    }

    /// <summary>
    /// intにする
    /// </summary>
    public static int _intValue(this string str) {
        string str_t = str;

        if (str_t == "" || str_t == null) {
            return 0;
        }
        if (str_t._indexOf(".") != -1) {
            ArrayList strArray = str._split(".");
            str_t = strArray[0].ToString();
        }       

        return Convert.ToInt32(str_t);
    }

    /// <summary>
    /// doubleにする
    /// </summary>
    public static double _doubleValue(this string str) {
        return Convert.ToDouble(str);
    }

    /// <summary>
    /// longにする
    /// </summary>
    public static long _longValue(this string str) {
        return Convert.ToInt64(str);
    }
        
    /// <summary>
    /// floatにする
    /// </summary>
    public static float _floatValue(this string str) {
        float f = float.Parse(str, CultureInfo.InvariantCulture.NumberFormat);
        return f;
    }      
        

    /// <summary>
    /// DateTimeにする
    /// </summary>
    public static DateTime _dateTimeValue(this string str) {
        DateTime dt1 = DateTime.Parse(str);
        return dt1;
    }




    /// <summary>
    /// stringWithFormat
    /// </summary>
    public static string _stringWithFormat(this string str, params object[] args) {

        if (args.Length > 0) {
            ArrayList argsArray = new ArrayList();
            foreach (dynamic argsStr in args) {
                argsArray.Add(argsStr.ToString());
            }
            var tempStr = String.Format(str, argsArray.ToArray());
            return tempStr;
        }
  
        return str + String.Format(str, args);
    }

    public static string _add(this string source,string str) {
        return source + str;
    }

   
        
    public static int _indexOf(this string source,string test) {
        return source.IndexOf(test);
    }


	/*
    public static int _indexOf(this NSObject source, string test) {
        return ((string)source).IndexOf(test);
    }
	*/

    /// <summary>
    /// split
    /// </summary>
    public static ArrayList _split(this string source, string test) {

        ArrayList newArray = new ArrayList();
        string[] splitArray =  source.Split(new string[] { test }, StringSplitOptions.None);
        if (splitArray.Length > 0) {
            foreach(string str in splitArray){
                newArray.Add(str);
            }
        }
        return newArray;
    }

    public static string _replaceReplaceStr(this string source, string test, string test2) {
        return source.Replace(test, test2);
    }
    
    public static string _replaceRegReplaceStr(this string source, string patarn, string test, string test2) {
        Regex regex = new Regex(patarn, RegexOptions.Singleline);
        return source.Replace(test, test2);
    }

    public static string _addStr(this string source, string test) {
        return source + test;
    }

    /// <summary>
    /// 端の指定した文字を削除する
    /// </summary>
    static public string _trimEnd(this string input, string suffixToRemove) {
        if (input != null && suffixToRemove != null && input.EndsWith(suffixToRemove)) {
            return input.Substring(0, input.Length - suffixToRemove.Length);
        }
        return input;
    }

    /// <summary>
    /// 半角だけかどうか　全角が判定
    /// </summary>
    static public Boolean _isHankakuCheck(this string str) {
        byte[] byte_data = System.Text.Encoding.GetEncoding(932).GetBytes(str);
        if (byte_data.Length == str.Length) {
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// 大文字にする
    /// </summary>
    static public string _oomojiFirst(this string str, params object[] args) {

        if (args.Length > 0) {
            foreach (string a in args) {
                str = str + a;                
            }
        }
        return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str);
    }


}


