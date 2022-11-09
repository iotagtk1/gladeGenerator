using System;

namespace signalGenerator2
{
    class Program
    {
        static void Main(string[] args)
        {

            clsGtkCsParse.Instance();

            /*
             Update the CS file

             Use the decompiler dnSpy to output the Dll to a Cs file
             */
            
            #if DEBUG                                
                          
                            
            #endif

            if (clsFolder._isFolder(clsPath._convertAbsolutFilePath(clsGtkCsParse.Instance().GtkFolder)) && 
                clsFolder._isFolder(clsPath._convertAbsolutFilePath(clsGtkCsParse.Instance().GdkFolder))
            ){
                clsGtkCsParse.Instance()._parseGtk();
                clsGtkCsParse.Instance()._parseGdk();      
            }
            else
            {
                Console.Write("ディレクトリがない");
            }

        }
    }
}