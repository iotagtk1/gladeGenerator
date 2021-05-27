using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
//using Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo.Create;
//namespace clsStringEx {

    

/*
 * 
Public 	パブリックメンバを検索の対象に加える。 	16
NonPublic 	パブリックでないメンバを検索の対象に加える。 	32
Instance 	非静的メンバ（インスタンスメンバ）を検索の対象に加える。 	4
Static 	静的メンバを検索の対象に加える。 	8
DeclaredOnly 	継承されたメンバを検索の対象にしない。 	2
FlattenHierarchy 	階層構造の上位の静的メンバを検索の対象に加える。 	64

* MemberInfo[] members = t.GetMembers(
BindingFlags.Public | BindingFlags.NonPublic |
BindingFlags.Instance | BindingFlags.Static |
BindingFlags.DeclaredOnly);     * 
pi.GetGetMethod();
* */

public static class objectExtensions {

    /// <summary>
    /// com用のプロパティを取得し　型の判定に使う
    /// </summary>
    public static ArrayList _getComProperties(this object obj) {
        ArrayList array = new ArrayList();
        foreach (PropertyDescriptor descrip in TypeDescriptor.GetProperties(obj)) {
            array.Add(descrip.Name);
        }
        return array;
    }

    /// <summary>
    /// com用のクラスの名前を取得し　型の判定に使う
    /// </summary>
    public static string _getComClassName(this object obj) {
        string className = TypeDescriptor.GetClassName(obj);
        return className;
    }



    /// <summary>
    /// intにする
    /// </summary>
    public static int _intValue(this object obj) {
        return obj.ToString()._intValue();
    }


    /// <summary>
    /// longにする
    /// </summary>
    public static long _longValue(this object obj) {
        return obj.ToString()._longValue();
    }
    
    

    /// <summary>
    /// flaotにする
    /// </summary>
    public static float _floatValue(this object obj) {
        return obj.ToString()._floatValue();
    }

    /// <summary>
    /// プロパティをsetする
    /// </summary>
    public static void _setSelector_Field(this object obj1, string propertyName, dynamic value) {

            Type t = obj1.GetType();
            FieldInfo fi = t.GetField(propertyName,
            BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.GetField
            | BindingFlags.SetProperty | BindingFlags.SetField
            | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static |
            BindingFlags.DeclaredOnly);

            if (fi == null) {
                Console.WriteLine("   fi null " + propertyName);
            } else {
                fi.SetValue(obj1, value);
            }

        }


        /// <summary>
        ///  型からフィールド名を取得する  typeof(型)._performSelector_Field_fromKata();
        /// </summary>
        /// <param name="obj">typeof(型)</param>
        public static ArrayList _get_FieldList_fromKata(this Type obj) {
            
            Type magicType = obj;

            FieldInfo[] myPropInfo = magicType.GetFields(
                       BindingFlags.Public | BindingFlags.NonPublic |
                       BindingFlags.Instance | BindingFlags.Static |
                       BindingFlags.DeclaredOnly);

            ArrayList filedNameArray = new ArrayList();
            foreach (FieldInfo a in myPropInfo) {
                filedNameArray.Add(a.Name);
            }
            return filedNameArray;
        }



        /// <summary>
        /// メソッドを実行する
        /// </summary>
        public static object _performSelector_Field(this object obj, string filed) {

            Type magicType = obj.GetType();

            FieldInfo f2 = magicType.GetField(filed,
                       BindingFlags.Public | BindingFlags.NonPublic |
                       BindingFlags.Instance | BindingFlags.Static |
                       BindingFlags.DeclaredOnly);

            object resobj2 = f2.GetValue(obj);
            return resobj2;
        
        }

        /// <summary>
        ///  メソッドがあるかどうか
        /// </summary>
        public static Boolean _respondsToSelector_Field(this object obj, string filed) {

            Type magicType = obj.GetType();
            FieldInfo mi = magicType.GetField(filed, BindingFlags.Public | 
                BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static |
            BindingFlags.DeclaredOnly);
            if (mi != null) {
                return true;
            }

            return false;
        }




        public static object _deepCopy(this object target) {

            object result;
            BinaryFormatter b = new BinaryFormatter();
            MemoryStream mem = new MemoryStream();

            try {
                b.Serialize(mem, target);
                mem.Position = 0;
                result = b.Deserialize(mem);
            } finally {
                mem.Close();
            }
            return result;
        }

        /// <summary>
        /// プロパティ一覧を取得する 型から  typeof(型)._getPropertyList_fromKata();
        /// </summary>
        public static ArrayList _getPropertyList_fromKata(this Type obj) {

            Type magicType = obj;

            PropertyInfo[] myPropInfo = magicType.GetProperties(
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static |
            BindingFlags.DeclaredOnly);

            ArrayList propertyNameArray = new ArrayList();
            foreach (PropertyInfo a in myPropInfo) {
                propertyNameArray.Add(a.Name);
            }
            return propertyNameArray;
        }

        /// <summary>
        /// プロパティ一覧を取得する
        /// </summary>
        /// 
        public static ArrayList _getPropertyList(this object obj) {

            Type magicType = obj.GetType();

            PropertyInfo[] myPropInfo = magicType.GetProperties(
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static |
            BindingFlags.DeclaredOnly);

            ArrayList propertyNameArray = new ArrayList();
            foreach (PropertyInfo a in myPropInfo) {
                propertyNameArray.Add(a.Name);
            }
            return propertyNameArray;
        }

        /// <summary>
        /// プロパティをgetする　実行する
        /// </summary>
        public static object _performSelector_Property(this object obj, string propertyName) {
            
            Type magicType = obj.GetType();
            PropertyInfo pi = magicType.GetProperty(propertyName,
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static |
            BindingFlags.DeclaredOnly);

            MethodInfo getMethod = pi.GetGetMethod();
            object result = getMethod.Invoke(obj, null);


            return result;
        }


        /// <summary>
        /// プロパティをsetする　 settter getter含む
        /// </summary>
        public static void _setSelector_Property(this object obj1, string propertyName ,dynamic value) {

            Type t = obj1.GetType();
            PropertyInfo pi = t.GetProperty(propertyName,
            BindingFlags.Public  
            | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static |
            BindingFlags.DeclaredOnly); 

            MethodInfo setMethod = pi.GetSetMethod();
            setMethod.Invoke(obj1, new Object[1] { value });

        }

        /// <summary>
        /// プロパティがあるかどうか settter getter含む
        /// </summary>
        public static Boolean _respondsToSelector_Property(this object obj, string property) {

            Type magicType = obj.GetType();           

            PropertyInfo mi = magicType.GetProperty(property,
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static |
            BindingFlags.DeclaredOnly);

            if (mi != null) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// メソッドを実行する
        /// </summary>
        public static object _performSelector_Method(this object obj, string method) {
            Type magicType = obj.GetType();
            MethodInfo mi = magicType.GetMethod(method,
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static |
            BindingFlags.DeclaredOnly);
            object result = mi.Invoke(method,null);

		return result;
        }
    
    
        /// <summary>
        /// メソッドを実行する
        /// </summary>
        public static object _performSelector_Method(this object obj, string method,dynamic objct1) {
    
            Type magicType = obj.GetType();

            MethodInfo mi = magicType.GetMethod(method,
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static |
                BindingFlags.DeclaredOnly);

            object result = mi.Invoke(method , new Object[1] { objct1 });

            return result;
        }

        /// <summary>
        ///  メソッドがあるかどうか
        /// </summary>
        public static Boolean _respondsToSelector_Method(this object obj, string method) {

            Type magicType = obj.GetType();
            MethodInfo mi = magicType.GetMethod(method,
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.Static |
            BindingFlags.DeclaredOnly);
            if (mi != null) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 指定したクラスがあるか
        /// </summary>
        public static Boolean _isKindOfClass(this object obj , Type a) {

            if (obj.GetType().ToString() == a.FullName.ToString()) {
                return true;
            }
            return false;
        }



        public static Boolean _isEqual(this object obj, object a) {
            if (obj == a) {
                return true;
            }
            return false;
        }   


        public static Boolean _boolValue(this object obj) {
            return Convert.ToBoolean(obj);
        }    
       
    
        public static int IntValue(this object obj) {
            return Convert.ToInt32(obj);
        }

        public static int _indexOf(this object obj, string searchWord) {
            return ((string)obj).IndexOf(searchWord);
        }


    







}

//}
