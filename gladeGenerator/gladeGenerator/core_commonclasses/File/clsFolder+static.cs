using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Drawing.Design;

public partial class clsFolder {

    /// <summary>
    /// Startupパスの取得 
    /// </summary>
    static public string _getStartupPath() {
        var StartupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        return StartupPath;
    }

    /// <summary>
    /// DesktopDirectoryパスの取得 
    /// </summary>
    static public string _getDesktopDirectoryPath() {
        var LocalApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        return LocalApplicationData;
    }

    /// <summary>
    /// LocalApplicationDataパスの取得 C:\Users\UserName\AppData\Local　\NanaSoft\nanananaRssReader
    /// </summary>
    static public string _getLocalApplicationDataPath() {
        var LocalApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        return LocalApplicationData;
    }


    /// <summary>
    /// Profileパスの取得 
    /// </summary>
    static public string _getLocalUserProfilePath() {
        var LocalApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        return LocalApplicationData;
    }
       

    /// <summary>
    /// ApplicationDataパスの取得 C:\Users\UserName\AppData\Roaming
    /// </summary>
    static public string _getApplicationDataPath() {
        var ApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        return ApplicationData;
    }

    /// <summary>
    /// テンプレートフォルダパス C:\Users\UserName\AppData\Roaming\Microsoft\Windows\Templates
    /// </summary>
    static public string _getTemplateFolderPath() {
        var ApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.Templates);
        return ApplicationData;
    }

    /// <summary>
    /// 共通テンプレートフォルダパス C:\ProgramData\Microsoft\Windows\Templates
    /// </summary>
    static public string _getCommonTemplateFolderPath() {
        var ApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.CommonTemplates);
        return ApplicationData;
    }

    /// <summary>
    /// プログラムフォルダパス \C:\Program Files
    /// </summary>
    static public string _getProgramFilesFolderPath() {
        var programFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        return programFilesPath;
    }

   /// <summary>
    /// フォルダ名を取得
    /// </summary>
    static public string _getFolderName(string dirPath) {
      string fileName = Path.GetDirectoryName(dirPath);
      return fileName;
    }  

    /// <summary>
    /// フォルダ一覧を取得する
    /// </summary>
    static public ArrayList _getFolders(string dirPath, string filter = "*") {
      string[] subFolders = System.IO.Directory.GetDirectories(
       dirPath, filter, System.IO.SearchOption.AllDirectories);
      ArrayList fileList = new ArrayList(subFolders);
      return fileList;
    }  





    /// <summary>
    /// ファイル一覧取得する
    /// </summary>
    static public ArrayList _getFiles(string dirPath,string filter = "") {
        string[] files = Directory.GetFiles(dirPath, filter, System.IO.SearchOption.AllDirectories);
        ArrayList fileList =  new ArrayList(files);
        return fileList;
    }  


    /*
     * ファイルを取得
  string[] files = Directory.GetFiles("c:\\", "*.cs");
     * ディレクトリを取得
 string[] dirs  = Directory.GetDirectories("c:\\", "*Microsoft*");
     * 
 string[] both  = Directory.GetFileSystemEntries("c:\\", "??");
     * 
     * ファイル名を詳細に
     * DirectoryInfo di = new DirectoryInfo("c:\\");
 FileInfo[] fis        = di.GetFiles("*.cs",System.IO.SearchOption.AllDirectories);
     */

    /// <summary>
    /// ディレクトリがあるかどうか
    /// </summary>   
    static public Boolean _isFolder(string dirPath) {
       if (Directory.Exists(dirPath)) {
           return true;
       }
       return false;
   }

    /// <summary>
    /// ディレクトリを作成する
    /// </summary>
    static public void _mkDirectory(string path) {
        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }      
    }


    /// <summary>
    /// ディレクトリ削除
    /// </summary>
    static public void _rmDirectory(string filePath) {        

        DeleteDirectory(new System.IO.DirectoryInfo(filePath));
   }

    public static void DeleteDirectory(System.IO.DirectoryInfo hDirectoryInfo) {
        // すべてのファイルの読み取り専用属性を解除する
        foreach (System.IO.FileInfo cFileInfo in hDirectoryInfo.GetFiles()) {
            if ((cFileInfo.Attributes & System.IO.FileAttributes.ReadOnly) == System.IO.FileAttributes.ReadOnly) {
                cFileInfo.Attributes = System.IO.FileAttributes.Normal;
            }
        }

        // サブディレクトリ内の読み取り専用属性を解除する (再帰)
        foreach (System.IO.DirectoryInfo hDirInfo in hDirectoryInfo.GetDirectories()) {
            DeleteDirectory(hDirInfo);
        }

        // このディレクトリの読み取り専用属性を解除する
        if ((hDirectoryInfo.Attributes & System.IO.FileAttributes.ReadOnly) == System.IO.FileAttributes.ReadOnly) {
            hDirectoryInfo.Attributes = System.IO.FileAttributes.Directory;
        }

        // このディレクトリを削除する
        hDirectoryInfo.Delete(true);
    }



    //ディレクトリのコピー
    static public void _directoryCopy(string sourceDicPath, string copyDicPath) {
      DirectoryInfo sourceDirectory = new DirectoryInfo(sourceDicPath);
      DirectoryInfo destinationDirectory = new DirectoryInfo(copyDicPath);

      //コピー先のディレクトリがなければ作成する
      if (destinationDirectory.Exists == false) {
        destinationDirectory.Create();
        destinationDirectory.Attributes = sourceDirectory.Attributes;
      }

      //ファイルのコピー
      foreach (FileInfo fileInfo in sourceDirectory.GetFiles()) {
        //同じファイルが存在していたら、常に上書きする
        fileInfo.CopyTo(destinationDirectory.FullName + @"\" + fileInfo.Name, true);
      }

      //ディレクトリのコピー（再帰を使用）
      foreach (DirectoryInfo directoryInfo in sourceDirectory.GetDirectories()) {
        _directoryCopy(directoryInfo.FullName, destinationDirectory.FullName + @"\" + directoryInfo.Name);
      }
    }


}

