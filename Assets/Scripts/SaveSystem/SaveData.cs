using UnityEngine;


[System.Serializable]
public class SaveData
{
    public static SaveData Instance;
    private static SaveData _instance { get
        {
            if (Instance == null)
            {
                Instance = new SaveData();
            }

            return Instance;
        } }

    
}


[System.Serializable]
public class ObjectTransformData
{
    public float[] position = new float[3];
    public float[] rotation = new float[3];
    public float[] scale = new float[3];    

    public float[] color = new float[3];

    //adding data with basic values to be able to seriallize them
}

