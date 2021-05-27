using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Text.Json;

public static partial class ArrayDicExtensions {

    /// <summary>
    /// 文字列か
    /// </summary>
    static public string _toStr(this String[] obj, string splitStr = "") {

        string newStr = "";
        foreach (string key in obj) {   
            newStr += key + splitStr;            
        }
        newStr = newStr._trimEnd(splitStr);
        return newStr;
    }

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
    /// _toJson
    /// </summary>
    public static string _toJson(this ArrayList source) {
            var json = JsonSerializer.Serialize(source);
            return json;
        }

        /// <summary>
        /// _remove_value
        /// </summary>
        /// <param name="array" >この中身が削除される</param>
        /// <param name="exceptArray">exceptArrayがあれば削除する</param>
        public static ArrayList _remove_value(this ArrayList array, ArrayList exceptArray) {

            foreach (var a in exceptArray) {
                if (array.Contains(a)) {
                    array.Remove(a);
                }
            }   
            return array;
        }


        /// <summary>
        /// _getArray_from_hash
        /// </summary>
        public static ArrayList _getArray_from_hash(this ArrayList source, string key) {
             ArrayList testArray = new ArrayList();
             foreach (Hashtable h in source) {
                if (h.ContainsKey(key)) {
                    testArray.Add(h[key]);
                }
             }
             return testArray;
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
        /// _indexObject
        /// </summary>
        public static string _safeSearch(this ArrayList source, string searchStr) {

          foreach(string str in source) {
            if(str._indexOf(searchStr) != -1) {
              return str;            
            }          
          }

          return "";
        }

        /// <summary>
        /// _indexObject
        /// </summary>
        public static Boolean _safeSearch_bool(this ArrayList source, string searchStr) {

            foreach (string str in source) {
                if (searchStr._indexOf(str) != -1) {
                    return true;
                }
            }

            return false;
        }

    /// <summary>
    /// _indexObject
    /// </summary>
    public static Boolean _searchValue(this ArrayList source, dynamic searchValue) {
    
            foreach(dynamic str in source) {
                if(str == searchValue) {
                    return true;            
                }          
            }
            return false;
        }


        /// <summary>
        /// SafeIndex
        /// </summary>
        public static Boolean _safeIndexOf(this ArrayList source, int index) {

            if (index < 0) {
                return false;
            }

            if (index > source.Count-1) {
                return false;
            }
            return true;            
        }

        /// <summary>
        /// SafeIndex
        /// </summary>
        public static Boolean _safeIndexOf(this string[] source, int index) {

            if (index < 0) {
                return false;
            }

            if (index > source.Length - 1) {
                return false;
            }
            return true;
        }    

        /// <summary>
        /// シリアライズ
        /// </summary>
        static public string _toJson(this Hashtable obj) {
            var json = JsonSerializer.Serialize(obj);
            return json;
        }

        /// <summary>
        /// ユニーク
        /// </summary>
        static public ArrayList _unique(this ArrayList obj) {

            ArrayList myArrayList = new ArrayList();
            myArrayList.AddRange(obj.ToArray().Distinct().ToArray());

            return myArrayList;
        }

        /// <summary>
        /// 文字列をつなげる
        /// </summary>
        public static string _join(this ArrayList array, string separator) {

            string[] sArray = (string[])array.ToArray(typeof(string));

            string str = string.Join(separator, sArray);

            return str;
        }


        /// <summary>
        /// 文字列をつなげる
        /// </summary>
        public static string _join_long(this ArrayList array, string separator) {


        long[] myArray = array.OfType<long>().ToArray();
        string str = string.Join(separator, myArray);

        return str;
        }

    /// <summary>
    /// コピーる
    /// </summary>
    public static ArrayList _copy(this ArrayList a) {            
            return (ArrayList)a.Clone();
        } 

        public static void _removeObjectForKey(this Hashtable h, string key) {
            h.Remove(key);
        }
	/*
        public static void _setObjectForKey(this Hashtable h, NSObject value, string key) {            
            h.Add(key,value);
        }
        /// <summary>
        /// Obj-cから用
        /// </summary>
        public static NSObject _objectForKey(this Hashtable h, string test) {
            return h[test];
        }
	*/
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


