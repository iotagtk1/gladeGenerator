using System;
using System.Collections.Generic;

namespace gladeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                clsArgsConfig.Instance();

                List<string> a = new List<string>();
                a.Add("-projectName");
                a.Add("hatenaApp");
                a.Add("-saveDir");
                a.Add("/home/ita/C#/App/hatenaApp/");
                a.Add("-fileDir");
                a.Add("/home/ita/C#/App/hatenaApp/hatenaApp/testTest.glade");
                args = a.ToArray();

                clsArgsConfig.Instance()._setArgs(args);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (!clsArgsConfig.Instance()._validateCommandKey())
            {
                return;
            }

            try
            {
                //引数
                clsSignalsData.Instance();

                clsPartsParse clsPartsParse1 = new clsPartsParse();

                clsPartsParse1._parsePrjectFolder(clsArgsConfig.Instance().FileDirPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}