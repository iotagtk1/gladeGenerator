using System;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using IniParser;
using IniParser.Model;

/*
 * Install-Package INIFileParserDotNetCore
 * using IniParser;
 * using IniParser.Model;
 * clsIniFile clsIniFile1 = new clsIniFile("./test.ini");
 * clsIniFile1["UI","fullscreen"] = "true";
*/
public class clsIniFile
{
    private string iniFileName = null;
    private FileIniDataParser parser = null;
    private IniData data = null;
    
    public static clsIniFile singlton;
        
    public static clsIniFile _sharedObject(string path) {
        if (singlton == null) {
            singlton = new clsIniFile(path);
        }
        return singlton;
    }

    public clsIniFile(string filePath) {
        
        parser = new FileIniDataParser();
        iniFileName = filePath;
        
        try{
            data = parser.ReadFile(iniFileName);
        }
        catch (Exception e)
        {
            if (data == null)
            {
                data = new IniData();
                parser.WriteFile(iniFileName, data);
            }
        }

    }

    public string this[string section,string key] {
        set
        {
            try
            {
                data[section][key] = value;
                parser.WriteFile(iniFileName, data);
            }
            catch (Exception e)
            {
                
            }
        }
        get {
            try
            {
                if (data[section][key] == null)
                {
                    return "";
                }
                
                return data[section][key];
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}




