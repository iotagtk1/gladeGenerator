using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Specialized;
using System.Resources;

public partial class clsFile {

    /// <summary>
    /// ファイルを移動する
    /// </summary>
    static public bool _move_static(string filePath_t, string movePath_t) {

        string dirPath = Path.GetDirectoryName(movePath_t);

        if (!Directory.Exists(dirPath)) {
            Directory.CreateDirectory(dirPath);
        }

        if (File.Exists(filePath_t) && filePath_t != "" && movePath_t != "" && 
            !File.Exists(movePath_t)) {
            File.Move(filePath_t, movePath_t);
            return true;
        } else {
            if (!File.Exists(filePath_t)) {
                Console.WriteLine("コピー元にファイルなし　" + movePath_t + " があります");
            }
            if (File.Exists(movePath_t)) {
                Console.WriteLine("コピー先にファイルあり　" + movePath_t + " があります");
            }
        }

        
        return false;
    }

    /// <summary>
    /// ファイルをコピーする
    /// </summary>
    static public bool _copy_static(string filePath_t, string movePath_t) {

        string dirPath = Path.GetDirectoryName(movePath_t);

        if (!Directory.Exists(dirPath)) {
            Directory.CreateDirectory(dirPath);
        }

        if (File.Exists(filePath_t) && filePath_t != "" && movePath_t != "") {
            File.Copy(filePath_t, movePath_t, true);
            return true;
        } else {           
            Console.WriteLine("コピー元　" + filePath_t + " がありません。");
        }

        if (!File.Exists(movePath_t)) {
            Console.WriteLine("コピー先　" + movePath_t + " がありません。");
        }
        return false;
    }



    /// <summary>
    /// 文字コードを変換する
    /// </summary>
    static public string _covnertSJIS_To_UTF8(string filePath) {

        Encoding srcEncoding = Encoding.GetEncoding("shift_jis");
        Encoding dstEncoding = Encoding.UTF8;
        using (var reader = new StreamReader(filePath, srcEncoding)) {
            string newStr = "";
            string line = null;
            while ((line = reader.ReadLine()) != null) {
                String convertedLine =
                    dstEncoding.GetString(
                        System.Text.Encoding.Convert(
                            srcEncoding,
                            dstEncoding,
                            srcEncoding.GetBytes(line)));

                newStr += convertedLine + Environment.NewLine;
            }

            return newStr;
        }

    }


    /// <summary>
    /// ファイルがあるかどうか
    /// </summary>
    static public Boolean _isFile(string filePath) {
        if (File.Exists(filePath)) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// リソース一覧 見当たらない場合はここを実行すること
    /// </summary>
    static public ArrayList _getResourceList() {

        System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
        string[] resnames = myAssembly.GetManifestResourceNames();

        ArrayList array = new ArrayList();

        foreach (string res in resnames) {
            Console.WriteLine("{0}", res);
            array.Add(res);
        }

        return array;
    }

    /// <summary>
    /// exeのあるパス
    /// </summary>
    static public string _getExePath() {

        System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetEntryAssembly();
        string path = Path.GetDirectoryName(myAssembly.Location);
    
        return path;
    }  
    
    /// <summary>
    /// exeのあるパスとファイル名をリプレースする
    /// </summary>
    static public string _getExePath_replace(string fileName) {

        System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetEntryAssembly();
        string path = Path.GetDirectoryName(myAssembly.Location);
    
        return path + "/" + fileName;
    }    
    
    /// <summary>
    /// 埋め込みソースを取得する　名前空間も含めること
    /// </summary>
    /// <param name="NameSpace">名前空間</param>
    static public string _getResource(string fileName) {

        try {
            /*
             *  string resoseFile = "{0}.lang.{1}.json"._stringWithFormat(
          clsUtility._getNameSpace(this),
          Thread.CurrentThread.CurrentUICulture.Name);      
             */

            //現在のコードを実行しているAssemblyを取得
            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();

            //ネームスペースがkj.sample、リソース名がtest.xmlの場合
            StreamReader sr = new StreamReader(myAssembly.GetManifestResourceStream(fileName), Encoding.UTF8);

            string s = sr.ReadToEnd();

            sr.Close();

            return s;

        } catch (Exception en) {

            clsFile._getResourceList();
            clsFile._console_static(en);
        }

        return "";
    }


    /// <summary>
    /// load
    /// </summary>
    static public string _load_static(string filePath) {
        var errorMes = "";
        StreamReader sr = null;

        var encodeCode = "UTF-8";

        if (filePath == "") {
            return "";
        }
        if (!File.Exists(filePath) && filePath != "") {
            errorMes = filePath + " がありません。";
            return "";
        }

        sr = new StreamReader(filePath, System.Text.Encoding.GetEncoding(encodeCode));
        string s = sr.ReadToEnd();
        sr.Close();

        return s;

    }

    /// <summary>
    /// バイナリーarray読み込み
    /// </summary>
    static public byte[] _fileToByteArray(string fileName) {
        byte[] buff = null;
        FileStream fs = new FileStream(fileName,
                                       FileMode.Open,
                                       FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);
        long numBytes = new FileInfo(fileName).Length;
        buff = br.ReadBytes((int)numBytes);
        return buff;
    }

    /// <summary>
    /// バイナリーarray読み込み
    /// </summary>
    static public object _loadArrayBinaryData(string filePath) {

        FileStream fs = new FileStream(filePath,
            FileMode.Open,
            FileAccess.Read);
        BinaryFormatter f = new BinaryFormatter();

        object obj = f.Deserialize(fs);
        fs.Close();

        return obj;

    }
    /// <summary>
    /// バイナリー保存
    /// </summary>
    static public void _saveArrayBinaryData(object al, string filePath) {


        FileStream fs = new FileStream(filePath,
        FileMode.Create,
        FileAccess.Write);
        BinaryFormatter bf = new BinaryFormatter();
        //シリアル化して書き込む
        bf.Serialize(fs, al);
        fs.Close();

    }


    /// <summary>
    /// ルートパスを取得する
    /// </summary>
	static public string _getRootFilePath(string filePath) {

        string rootDir = "";

        rootDir = Path.GetPathRoot(filePath);

        return rootDir;
    }
    
    /// <summary>
    /// 相対パスを絶対パスにする
    /// </summary>
    static public string _getAbsoluteFilePath(string filePath) {

        string rootDir = "";

        rootDir = Path.GetFullPath(filePath);

        return rootDir;
    } 
    

    /// <summary>
    /// 指定したパスに保存する
    /// </summary>
    static public void _saveFilePath(string text, string filePath ,string encodeCode = "") {

        StreamWriter sw = null;

        string rootDir = Path.GetPathRoot(filePath);

        if (rootDir != "" && Directory.Exists(rootDir) == false) {
            Console.WriteLine("ディレクトリがない ドライブからの情報がない   ");

            // clsFile._mkMesBox_OkCancel("ドライブからの情報がない ", "エラー");
            return;
        }
        //utf-8
        //shift_jis
       
        encodeCode = encodeCode != "" ? encodeCode : "UTF-8";

        var dirName = Path.GetDirectoryName(filePath);

        if (Directory.Exists(dirName) == false) {
            Directory.CreateDirectory(dirName);
        }
        if (!File.Exists(filePath) && filePath != "") {
            File.Create(filePath).Close();
        }
        sw = new StreamWriter(filePath, false, Encoding.GetEncoding(encodeCode));
        sw.Write(text);

        sw.Close();


    }

    /// <summary>
    /// ファイル情報
    /// </summary>
    static public Hashtable _getFileInfo(string filePath) {

        FileInfo fi2 = new FileInfo(filePath);

        Hashtable h = new Hashtable();

        h.Add("NSFileCreationDate", fi2.CreationTime);
        h.Add("NSFileModificationDate ", fi2.LastWriteTime);

        return h;

    }

    /// <summary>
    /// ファイルリスト取得する
    /// </summary>
    static public ArrayList _getFileList(string folderPath, string filter = "*", ArrayList ngDirArray = null,Boolean isAllDir = true) {

        ArrayList newArrayList = new ArrayList();

        if (Directory.Exists(folderPath)) {

            if (filter.IndexOf("*") == -1 && filter.IndexOf(".") != -1)
            {
                filter = "*" + filter;
            }

            string[] files = null;

            if (isAllDir) {
                files = Directory.GetFiles(folderPath, filter, System.IO.SearchOption.AllDirectories);
            } else {
                files = Directory.GetFiles(folderPath, filter, System.IO.SearchOption.TopDirectoryOnly);
            }            

            if (files.Length > 0) {
                foreach (string str in files) {

                    bool hitFlag = false;
                    if (ngDirArray != null) {
                        foreach (string ngPath in ngDirArray) {
                            if (str.IndexOf(ngPath) != -1) {
                                hitFlag = true;
                            }
                        }
                    }
                    if (!hitFlag) {
                        newArrayList.Add(str);
                    }
                }
            }

        } else {
            Console.WriteLine("フォルダーがない");
        }

        return newArrayList;

    }

    /// <summary>
    /// ファイルを削除する
    /// </summary>
    static public bool _deleteFile(string filePath) {

        if (File.Exists(filePath) && filePath != "") {
            File.Delete(filePath);
            return true;
        }

        return false;

    }

    static public void _console_static(Exception en) {
        string errorMes = "";
        Console.WriteLine(en.Message);
        Console.WriteLine(en.StackTrace);
        Console.WriteLine(en.TargetSite);
        errorMes += en.Message;

        System.Diagnostics.Debug.WriteLine(en.Message);

        //サーバーに送信したい
    }

    /// <summary>
    /// 一時フォルダのパスを取得する
    /// </summary>
    static public string _getTempPath() {

        string path = Path.GetTempPath();

        return path;
    }


    /// <summary>
    /// ユーザープロファイルのパスの取得
    /// </summary>
    static public string _getUserProfilePath() {
        return System.Environment.GetFolderPath(
            Environment.SpecialFolder.UserProfile);
    }



}

