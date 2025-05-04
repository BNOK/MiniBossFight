using UnityEngine;

public class Helpers
{
    public ObjectTransformData dataToSave;


    //example how to save data
    void ConvertData(Transform objectTransform)
    {
        dataToSave.position[0] = objectTransform.transform.position.x;
        dataToSave.position[1] = objectTransform.transform.position.y;
        dataToSave.position[2] = objectTransform.transform.position.z;
    }
}
