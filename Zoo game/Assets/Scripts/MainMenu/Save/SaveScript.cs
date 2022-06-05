using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveScript 
{
    [Header("Save Paths")]
    public static string saveBookLoc = "/scrapBook.save";//where the scrapBook settings get saved
    public static string saveSettingsLoc = "/scrapSettings.save";//where the settings get saved

    public static void saveBook(BookController oData)///called after something is unlocked for example
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + saveBookLoc;
        FileStream stream =  new FileStream(path,FileMode.Create);
        BookData data = new BookData(oData);
        formatter.Serialize(stream,data);
        stream.Close();
    }

    public static BookData loadBook()//call on start so to load in the saved data
    {
        string path = Application.persistentDataPath + saveBookLoc;
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream =  new FileStream(path,FileMode.Open);
            BookData data = formatter.Deserialize(stream) as BookData;
            stream.Close();
            return data;
        }else{
            Debug.Log("File not found" + path);
            return null;          
        }
    }

    //////settings
    public static void saveSettings(SettingsController oData)///call after setting data or when save button is pressed
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + saveSettingsLoc;
        FileStream stream =  new FileStream(path,FileMode.Create);
        SettingData data = new SettingData(oData);
        formatter.Serialize(stream,data);
        stream.Close();
    }

    public static SettingData loadSettings()//call on start so to load in the saved data
    {
        string path = Application.persistentDataPath + saveSettingsLoc;
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream =  new FileStream(path,FileMode.Open);
            SettingData data = formatter.Deserialize(stream) as SettingData;
            stream.Close();
            return data;
        }else{
            Debug.Log("File not found" + path);
            return null;          
        }
    }

}
