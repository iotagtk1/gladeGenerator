using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public partial class clsPath {

   static  private string errorMes;

   public clsPath() {
    
   }

    static public string _getFileNameNoExtension(string filePath) {
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        return fileName;
    }
    
    static public string _getFileName(string filePath) {
        string fileName = Path.GetFileName(filePath);
        return fileName;
    }

    /// <summary>
    /// ���݂̃p�X���΃p�X
    /// </summary>
    static public string _getAbsoluteDirPath() {
       string path = Path.GetFullPath("./");
       return path;
    }

    /// <summary>
    /// ���݂̃t�@�C���̐�΃p�X
    /// </summary>
    static public string _getAbsolutFilePath(string fileName) {    
          string path =  Path.GetFullPath("./"+fileName);
          return path;
   }

    /// <summary>
    /// ���΃p�X�ƃt�@�C���������΃p�X���擾����
    /// </summary>
    static public string _getSelectAbsolutFilePath(string selectDir , string fileName) {

      string path = Path.GetFullPath(selectDir + fileName);
      return path;
   }

     
   static  private void _console(Exception en) {
     Console.WriteLine(en.Message);
     Console.WriteLine(en.StackTrace);
     Console.WriteLine(en.TargetSite);
     errorMes += en.Message;

     System.Diagnostics.Debug.WriteLine(en.Message);
   }
 

 }

