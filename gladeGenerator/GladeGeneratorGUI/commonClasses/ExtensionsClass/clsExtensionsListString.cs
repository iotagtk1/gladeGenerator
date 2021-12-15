using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Text.Json;
using System.Xml.Serialization;

public static partial class ListStringExtensions {

        /// <summary>
        /// 文字列か
        /// </summary>
        static public string _toStr(this List<string> obj, string splitStr = "") {

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
        public static string _toJson(this List<string> source) {
            var json = JsonSerializer.Serialize(source);
            return json;
        }

        /// <summary>
        /// _remove_value
        /// </summary>
        /// <param name="array" >この中身が削除される</param>
        /// <param name="exceptArray">exceptArrayがあれば削除する</param>
        public static List<string> _remove_value(this List<string> array, List<string> exceptArray) {

            foreach (var a in exceptArray) {
                if (array.Contains(a)) {
                    array.Remove(a);
                }
            }   
            return array;
        }

        /// <summary>
        /// _indexObject
        /// </summary>
        public static string _safeSearch(this List<string> source, string searchStr) {

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
        public static Boolean _safeSearch_bool(this List<string> source, string searchStr) {
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
        public static Boolean _searchValue(this List<string> source, dynamic searchValue) {
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
        public static Boolean _safeIndexOf(this List<string> source, int index) {

            if (index < 0) {
                return false;
            }

            if (index > source.Count-1) {
                return false;
            }
            return true;            
        }

        /// <summary>
        /// ユニーク
        /// </summary>
        static public List<string> _unique(this List<string> obj) {

            List<string> myArrayList = new List<string>();
            myArrayList.AddRange(obj.ToArray().Distinct().ToArray());

            return myArrayList;
        }

        /// <summary>
        /// 文字列をつなげる
        /// </summary>
        public static string _join(this List<string> array, string separator) {

            var str = string.Join( separator, array );

            return str;
        }

        /// <summary>
        /// コピー
        /// </summary>
        public static List<string> _copy(this List<string> array_old) { 

            List<string> tlist =  array_old.Select(item => (string)item.Clone()).ToList();
            
            return tlist;
        }

}


