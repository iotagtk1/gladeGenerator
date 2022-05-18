using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Text.Json;
using System.Xml.Serialization;

public static partial class HashExtensions {


    
    /// <summary>
    /// シリアライズ
    /// </summary>
    static public string _toStr(this Hashtable obj,string splitStr = "") {

        string newStr = "";
        foreach (string key in obj.Keys) { 

            if (obj[key].GetType() == typeof(String)) {
                newStr += key + splitStr + obj[key].ToString() + splitStr;
            } else if (obj[key].GetType() == typeof(ArrayList)) {
                newStr += key + splitStr;
                foreach (string str2 in ((ArrayList)obj[key])) {
                    newStr += str2 + splitStr;
                }
            }
        }
        newStr = newStr._trimEnd(splitStr);
        return newStr;
    }

    /// <summary>
    /// _sort 数字を入れる
    /// </summary>
    public static ArrayList _sort(this Hashtable hash) {

        ArrayList keys = new ArrayList(hash.Keys);
        keys.Sort();

        ArrayList newHashtable = new ArrayList();
        foreach (var key in keys) {
            newHashtable.Add(hash[key]);
        }

        return newHashtable;

    }

    /// <summary>
    /// シリアライズ
    /// </summary>
    static public string _toJson(this Hashtable obj) {
        var json = JsonSerializer.Serialize(obj);
        return json;
    }

    public static void _removeObjectForKey(this Hashtable h, string key) {
        h.Remove(key);
    }
	
    /// <summary>　
    /// カウント　Obj-cから用
    /// </summary>
    public static int _count(this Hashtable h) {            
        return h.Count;
    }
    /// <summary>
    /// キーを取得する　Obj-cから用
    /// </summary>
    public static ArrayList _allKeys(this Hashtable h) {
        ArrayList list1 = new ArrayList(h.Keys);
        return list1;
    }


}