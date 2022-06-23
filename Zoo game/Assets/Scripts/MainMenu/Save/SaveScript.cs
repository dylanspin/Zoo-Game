using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveScript 
{
    [Header("Save Paths")]
    public static string saveBookLoc = "/scrapBook1.save";//where the scrapBook settings get saved
    public static string saveSettingsLoc = "/scrapSettings3.save";//where the settings get saved


    ///Book data
    public static void saveBook(BookController oData)///called after something is unlocked for example
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + saveBookLoc;//persistant path depends on the platform but for windows its here : %userprofile%\AppData\LocalLow\
        FileStream stream =  new FileStream(path,FileMode.Create);
        BookData data = new BookData(oData);
        formatter.Serialize(stream,data);//converts the data to be encrypted
        stream.Close();
    }

    public static BookData loadBook()//call on start so to load in the saved data
    {
        string path = Application.persistentDataPath + saveBookLoc;
        if(File.Exists(path))//if the file exist
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);
            BookData data = formatter.Deserialize(stream) as BookData;//decrypts the saved data
            stream.Close();
            return data;//returns the data from the saved BookData class
        }else{
            return null;          
        }
    }

    //////settings
    public static void saveSettings(SettingsController oData)///call after setting data or when save button is pressed
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + saveSettingsLoc;//persistant path depends on the platform but for windows its here : %userprofile%\AppData\LocalLow\
        FileStream stream =  new FileStream(path,FileMode.Create);
        SettingData data = new SettingData(oData);
        formatter.Serialize(stream,data);//converts the data to be encrypted
        stream.Close();
    }

    public static SettingData loadSettings()//call on start so to load in the saved data
    {
        string path = Application.persistentDataPath + saveSettingsLoc;
        if(File.Exists(path))//if the file exist
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);
            SettingData data = formatter.Deserialize(stream) as SettingData;//decrypts the saved data
            stream.Close();
            return data;//returns the data from the saved SettingData class
        }else{
            return null;          
        }
    }

}
