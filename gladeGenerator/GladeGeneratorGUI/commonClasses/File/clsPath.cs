using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public partial class clsPath {

   static  private string errorMes;

   public clsPath() {
    
   }

   /// <summary>
   ///  FilePathからファイル名を取得する 拡張なし
   /// </summary>
   /// <param name="filePath"></param>
   /// <returns></returns>
    static public string _getFileNameNoExtension(string filePath) {
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        return fileName;
    }
    
    /// <summary>
    /// FilePathからファイル名を取得する
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    static public string _getFileName(string filePath) {
        string fileName = Path.GetFileName(filePath);
        return fileName;
    }

    /// <summary>
    /// 現在の位置から絶対パスを取得する
    /// </summary>
    static public string _getAbsoluteDirPath() {
       string path = Path.GetFullPath("./");
       return path;
    }

    /// <summary>
    /// 相対パスを絶対パスにする
    /// </summary>
    static public string _getAbsolut_FromFilePath(string filePath) {    
          string path = Path.GetFullPath(filePath);
          return path;
    }
    
    /// <summary>
    /// 絶対パスにする
    /// </summary>
    static public string _getSelectAbsolutFilePath(string selectDir , string fileName) {

      string path = Path.GetFullPath(selectDir + fileName);
      return path;
   }

    /// <summary>
    /// FullPathに変換する
    /// </summary>
    /// <param name="dirPath"></param>
    /// <returns></returns>
    static public string _convertAbsolutFilePath(string dirPath) {

        string path = Path.GetFullPath(dirPath);
        return path;
    }

    static  private void _console(Exception en) {
     Console.WriteLine(en.Message);
     Console.WriteLine(en.StackTrace);
     Console.WriteLine(en.TargetSite);
     errorMes += en.Message;

     System.Diagnostics.Debug.WriteLine(en.Message);
   }
    
    /// <summary>
    /// ファイルパスからディレクトリパスを取得する
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    static public string _getDirPath_fromFilePath(string filePath) {

        string path = Path.GetDirectoryName(filePath);
        return path;
    }
    
 

 }

