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
                

                #if DEBUG                                
                              
                                
                #endif
                
                
                clsArgsConfig.Instance()._setArgs(args);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //引数の検証
            if (!clsArgsConfig.Instance()._validateCommandKey())
            {
                return;
            }

            try
            {
                clsPartsParse clsPartsParse1 = new clsPartsParse();

                clsPartsParse1._parsePrjectFolder(clsArgsConfig.Instance().FileDirPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}