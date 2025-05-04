using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SavingManager
{
    public static bool Save(string saveName, object saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if(!Directory.Exists(Application.persistentDataPath+"/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string savePath = Application.persistentDataPath + "/saves/" + saveName + ".save" ;

        FileStream file = File.Create(savePath);

        formatter.Serialize(file, saveData);

        file.Close();

        return true;
    }
}
