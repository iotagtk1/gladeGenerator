using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;


static public partial class clsUtility {



    public static string _getNameSpace() {
        return System.Reflection.Assembly.GetExecutingAssembly().EntryPoint.DeclaringType.Namespace;
    }



    static public System.Random rundam;
    /// <summary>
    /// ランダム変数を取得
    /// </summary>
    static public int _getRandomNum(int maxNum) {
        if (rundam == null) {
            rundam = new System.Random(10000);
        }
        int i1 = rundam.Next(1,maxNum);
        return i1;
    }

    /// <summary>
    /// コマンドラインを取得する
    /// </summary>
    static public string[] _getCommandLine() {

        string[] Commands = System.Environment.GetCommandLineArgs();

        return Commands;
    }

    /// <summary>
    /// ランダム変数
    /// </summary>
    static public int _getRandomNums(int minNum = 0, int maxNum = 1000) {

        Random cRandom = new System.Random();

        // 0 以上の乱数を取得する
        int iResult1 = cRandom.Next(minNum, maxNum);

        return iResult1;
    }


    /// <summary>
    /// 非同期タイマー  プロジェトファイルに定義すること
    /// <DefineConstants Condition = " '$(TargetFrameworkVersion)' == 'v4.0' " > RUNNING_ON_4 </ DefineConstants >
    /// <DefineConstants Condition=" '$(TargetFrameworkVersion)' != 'v4.0' ">NOT_RUNNING_ON_4</DefineConstants>
    /// </summary>

#if NOT_RUNNING_ON_4
    static public async void _timerAsync(int timerNum) {
        await Task.Run(() => {
            Thread.Sleep(timerNum);  // 重たい処理のつもり
        });
    }
#endif

    /// <summary>
    /// 置き換え　() に対し$1　$2  $3  $で指定()中身を丸ごと持ってこれる
    /// </summary>
    static public string _patarnReplace(string str, string patarn,string replaceStr) {

        //連続する同じ行を削除
        var str2 = Regex.Replace(str,patarn, replaceStr, RegexOptions.Singleline);

        return str2;
    }

    /// <summary>
    /// 置き換え　() に対し$1　$2  $3 
    /// </summary>
    static public string _patarnReplace_multi(string str, string patarn, string replaceStr) {

        //連続する同じ行を削除
        var str2 = Regex.Replace(str, patarn, replaceStr, RegexOptions.Multiline);

        return str2;
    }


    /// <summary>
    /// パターンマッチ エスケープ文字 \ 改行ありはSinglelineのほうがいい 括弧は１から　\	*	+	.	?	{　}  (　)  [　] ^	$	-	|	/　\を追加すること
    ///  RegexOptions.Singleline 改行込み　RegexOptions.Multi 改行コードが必要 kakko 1 2
    /// </summary>
    static public ArrayList _patarnMatch(string str , string patarn , int kakko = -1, int kakko2 = -1) {

        //正規表現パターンとオプションを指定してRegexオブジェクトを作成
        Regex r = new Regex(patarn, RegexOptions.Singleline);

        //TextBox1.Text内で正規表現と一致する対象をすべて検索
        MatchCollection mc = r.Matches(str);

        ArrayList array = new ArrayList();

        if (mc.Count == 0) {
         //   Console.WriteLine(" ヒットなし   ");
        }
                
        foreach (Match m in mc) {
            //正規表現に一致したグループと位置を表示
            if (kakko != -1) {
                array.Add(m.Groups[kakko].Value.ToString());
            }
            if (kakko2 != -1) {
                array.Add(m.Groups[kakko2].Value.ToString());
            }
        }

        return array;
    }


    /// <summary>
    /// 機種名を取得する
    /// </summary>
    static public string _getMachineName() {
        return Environment.MachineName;
    }

    /// <summary>
    /// ホスト名を取得する
    /// </summary>
    static public string _getHostName() {
        return System.Net.Dns.GetHostName();
    }

    /// <summary>
    /// 現在の時間を取得する（文字列）
    /// </summary>
    static public string _getNowDateTime_str() {
        return DateTime.Now.ToString();
    }

    /// <summary>
    /// 現在の時間を取得する
    /// </summary>
    static public DateTime _getNowDateTime() {
        return DateTime.Now;
    }

    /// <summary>
    /// タイムスタンプ
    /// </summary>
    static public long _geTimeStamp() {

        DateTime targetTime = DateTime.Now.ToUniversalTime();

        DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        // UNIXエポックからの経過時間を取得
        TimeSpan elapsedTime = targetTime - UNIX_EPOCH;

        // 経過秒数に変換
        return (long)elapsedTime.TotalSeconds;
    }

    /// <summary>
    /// エンコードを変更する
    /// </summary>
    static public string _convetEncodeType(string input,string encodeType) {

        /*
         * iso-2022-jp
         * utf-8
         * euc-jp
         * csISO2022JP
         */

        //ASCII エンコード
        //byte[] data = System.Text.Encoding.ASCII.GetBytes(input);

        //データがShift-JISの場合
        //byte[] data = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(input);

        //データがEUCの場合
        //byte[] data = System.Text.Encoding.GetEncoding("euc-jp").GetBytes(input);

        //データがunicodeの場合
        //byte[] data = System.Text.Encoding.Unicode.GetBytes(input);

        //データがutf-8の場合
        byte[] data = System.Text.Encoding.UTF8.GetBytes(input);

        //byte[] decbytes = Convert.FromBase64String(input);

        //Encoding enc = Encoding.GetEncoding(50220);
        Encoding enc = Encoding.GetEncoding(encodeType);

        string orgStr = enc.GetString(data);

        return orgStr;
    }

    /// <summary>
    /// インターバル　同期 1000 一秒
    /// </summary>
    static public void _setInterval(int timer) {
        System.Threading.Thread.Sleep(timer);
    }



    /// <summary>
    /// mk5にする
    /// </summary>
    /// 
    /*
    public static string _mkMd5(string srcStr, System.Text.Encoding enc) {
        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

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
    */



    /// <summary>
    /// base64をエンコードする
    /// </summary>
    static public string _encodeBase64(string str) {
        byte[] bytesD;
        bytesD = System.Text.Encoding.UTF8.GetBytes(str);

        string result;
        result = System.Convert.ToBase64String(bytesD);

        return result;
    }

    /// <summary>
    /// base64をデコードする
    /// </summary>
    static public string _decodeBase64(string str) {
    byte[] bs = System.Convert.FromBase64String(str);

    string result = System.Text.Encoding.UTF8.GetString(bs);

    return result;
   }
      

    /// <summary>
    /// 数字かどうか
    /// </summary>
    static public bool _isNumeric(string stTarget) {
      double dNullable;
      return double.TryParse(
          stTarget,
          System.Globalization.NumberStyles.Any,
          null,
          out dNullable
      );
    }


    /// <summary>
    /// 半角かどうか
    /// </summary>
    static public Boolean _isOneByteChar(string nihongo){
      byte[] byte_data = System.Text.Encoding.GetEncoding(932).GetBytes(nihongo);
      if(byte_data.Length == nihongo.Length) {
        return true;
      }
      //全角含む
      return false;
    }

    /// <summary>
    /// 例外処理を起こす
    /// </summary>
    static public void _mkApplicationException(string nihongo,Exception e){

      Console.WriteLine(e.Message);
      Console.WriteLine(e.StackTrace);
      Console.WriteLine(e.TargetSite);

      throw new ApplicationException(nihongo + e.Message, e);  
    }

    static public void _mkErrorMes( Exception e) {

        Console.WriteLine(e.Message);
        Console.WriteLine(e.StackTrace);
        Console.WriteLine(e.TargetSite);

    }




  }
