using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Text.Json;
using System.Xml.Serialization;

public static partial class ListExtensions1 {

    static public List<double?> _excepteNull_double(this List<double?> array)
    {
        List<double?> excepteNullArray = new List<double?>();

        foreach (double? doubleNum in array)
        {
            if (doubleNum != null)
            {
                excepteNullArray.Add(doubleNum.GetValueOrDefault()); 
            }
        }
        return excepteNullArray;
    }
    static public List<T> _slice<T>(this List<T> array,int index, int count) {
        
        if (index >= array.Count)
        {
            return null;
        }
        
        List<T> arrayCopy = array.GetRange (index, count);
        return arrayCopy;
    }

}