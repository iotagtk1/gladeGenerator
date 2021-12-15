using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Text.Json;
using System.Xml.Serialization;

public static partial class ArrayExtensions {

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
        /// _indexObject
        /// </summary>
        public static string _safeSearch(this ArrayList source, string searchStr) {

          foreach(string str in source) {
            if(str.IndexOf(searchStr) != -1) {
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
                if (searchStr.IndexOf(str) != -1) {
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
 
        /// <summary>
        /// 配列を分割する
        /// </summary>
        public static List<string[]> _splitArray(this ArrayList array2 , int splitNum)
        {
            string[] array = (string[])array2.ToArray(typeof(string));
            
            if (splitNum < array.Length-1 && splitNum > 0)
            {
                string[] firstarray, secondarray; 
                firstarray = new string[splitNum];
                secondarray = new string[array.Length - splitNum];
                Array.Copy(array, 0, firstarray, 0, splitNum);
                Array.Copy(array, splitNum, secondarray, 0, secondarray.Length);

                List<string[]> array_split = new List<string[]>();
                array_split.Add(firstarray);
                array_split.Add(secondarray);
                return array_split;
            }
            return null;
        }

    }


