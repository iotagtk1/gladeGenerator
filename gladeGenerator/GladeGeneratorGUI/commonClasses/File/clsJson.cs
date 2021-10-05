using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;


/*
public static partial class jsonExtensions {
    /// <summary>
    /// jObjectをstrにする
    /// </summary>
    static public string _toStr(this JObject jObj) {
        return jObj.ToString(Formatting.None);
    }
}
*/

public partial class clsJson {

    /// <summary>
    /// シリアライズ
    /// </summary>
   static public string _serialize(object obj) {
        var json = JsonSerializer.Serialize(obj);
        return json;
    }

    /// <summary>
    /// パース
    /// </summary>
    /*
    static public JObject _purse(string jsonStr)
    {

        var json =  JsonSerializer.Deserialize<string>(jsonStr);

        JObject json = JObject.Parse(jsonStr);

        return json;
    }
    */

    /// <summary>
    /// Jsonかどうか
    /// </summary>
    static public Boolean _isJsonString(string jsonStr) {

        if (jsonStr.IndexOf("{") != -1 && jsonStr.IndexOf("}") != -1 && jsonStr.IndexOf(":") != -1) {
            return true;
        }

        return false;
    }



}

