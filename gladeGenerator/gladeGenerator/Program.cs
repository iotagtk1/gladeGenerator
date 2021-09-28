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