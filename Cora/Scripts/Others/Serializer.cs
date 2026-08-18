using Microsoft.Win32;
using System.IO;

public class Serializer
{
    public static string SpecialPath
    {
        get
        {
            string returnPath = @"C:\";
            return Path.Combine(returnPath, @"Cora\");
        }
    }
    public static string UserPath
    {
        get
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cora");
        }
    }
    public static string DefaultPath
    {
        get
        {
            return Path.Combine(@"C:\Cora\");
        }
    }
    public static string SettingsPath
    {
        get
        {
            return Path.Combine(SpecialPath, @"Settings\");
        }
    }
    public static string ReportsPath
    {
        get
        {
            return Path.Combine(DefaultPath, @"Reports\");
        }
    }
    public static string SalesPath
    {
        get
        {
            return Path.Combine(DefaultPath, @"Sales\");
        }
    }
    public static string BudgesPath
    {
        get
        {
            return Path.Combine(DefaultPath, @"Budges\");
        }
    }
    public static string DataPath
    {
        get
        {
            return Path.Combine(DefaultPath, @"Data\");
        }
    }

    public static void CreateDirectories()
    {
        //Default Folder
        if (!Directory.Exists(DefaultPath)) Directory.CreateDirectory(DefaultPath);

        //Settings Folder
        if (!Directory.Exists(SettingsPath)) Directory.CreateDirectory(SettingsPath);

        //Sales Folder
        if (!Directory.Exists(SalesPath)) Directory.CreateDirectory(SalesPath);

        //Sales Folder
        if (!Directory.Exists(BudgesPath)) Directory.CreateDirectory(BudgesPath);

        //Reports Folder
        if (!Directory.Exists(ReportsPath)) Directory.CreateDirectory(ReportsPath);

        //Data Folder
        if (!Directory.Exists(DataPath)) Directory.CreateDirectory(DataPath);

        //User Folder
        if (!Directory.Exists(UserPath)) Directory.CreateDirectory(UserPath);
    }
    public static bool FileExists(string path)
    {
        return File.Exists(path);
    }
}