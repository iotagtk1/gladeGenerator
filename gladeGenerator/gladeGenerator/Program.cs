using System;

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
                //引数
                clsSignalsData.Instance();

                clsPartsParse clsPartsParse1 = new clsPartsParse();

                clsPartsParse1._parsePrjectFolder(clsArgsConfig.Instance().ProjectFolder);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}