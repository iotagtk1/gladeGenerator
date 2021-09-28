using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;

public class clsString {

    /// <summary>
    /// stream to string
    /// </summary>
    static public string _xmlToStream(Stream stream) {
        StreamReader reader = new StreamReader(stream);
        string text = reader.ReadToEnd();
        return text;
    }


    //端の指定した文字を削除する
    static public string _trimEnd(string input, string suffixToRemove) {
    if(input != null && suffixToRemove != null && input.EndsWith(suffixToRemove)) {
      return input.Substring(0, input.Length - suffixToRemove.Length);
    } 
    return input;
  }

  //HTMLエンコード
  static public string _htmlEncode(string html) {
    html = System.Web.HttpUtility.HtmlEncode(html);
    return html;
  }

  //HTMLデコード
  static public string _htmlDecode(string html) {
    html = System.Web.HttpUtility.HtmlDecode(html);
    return html;
  }

  //半角だけかどうか　全角が判定
  static public Boolean _isHankakuCheck(string str) {
    byte[] byte_data = System.Text.Encoding.GetEncoding(932).GetBytes(str);
    if (byte_data.Length == str.Length) {
      return true;
    } else {
      return false;
    }
  }

 //md5ファイル
  static public string md5EncodeString(string inputString) {

    MD5 md5EncryptionObject = new MD5CryptoServiceProvider();
    Byte[] originalStringBytes = System.Text.Encoding.UTF8.GetBytes(inputString);
    Byte[] encodedStringBytes = md5EncryptionObject.ComputeHash(originalStringBytes);
    System.Text.StringBuilder result = new System.Text.StringBuilder();
    foreach(byte b in encodedStringBytes) {
      result.Append(b.ToString("x2"));
    }
    return result.ToString();
  }

  static public string _addNumName(string fileName, string filePath) {
    if(Directory.Exists(filePath) == false) {
      return fileName;
    }
    if(Directory.Exists(filePath) == true) {
      string[] fileFullArray = Directory.GetFiles(filePath,"*",SearchOption.AllDirectories);

      List<string> fileArray = new List<string>();
      foreach(string fileFull in fileFullArray) {
        fileArray.Add(Path.GetFileName(fileFull));
      }

      string[] fileArray_sei = fileArray.ToArray();

      string[] fileNameArray = fileName.Split(new string[] { "." },StringSplitOptions.None);
      int i = 0;
      while(Array.IndexOf(fileArray_sei,fileName) != -1) {
        fileName = fileNameArray[0] + "(" + i.ToString() +")."+fileNameArray[1];
        i++;
      }
    }
    return fileName;
  }


    /// <summary>
    /// mk5にする
    /// </summary>
    static public string _md5(string srcStr) {
        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

        Encoding enc = new System.Text.UTF8Encoding(true, true);

        // md5ハッシュ値を求める
        byte[] srcBytes = enc.GetBytes(srcStr);
        byte[] destBytes = md5.ComputeHash(srcBytes);

        // 求めたmd5値を文字列に変換する
        System.Text.StringBuilder destStrBuilder;
        destStrBuilder = new System.Text.StringBuilder();
        foreach (byte curByte in destBytes) {
            destStrBuilder.Append(curByte.ToString("x2"));
        }

        // 変換後の文字列を返す
        return destStrBuilder.ToString();
    }








}

