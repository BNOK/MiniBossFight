using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] Player1Health;
    public Image[] Player2Health;


    void DecreaseHealth(int index, int health)
    {
        Image[] heartImage = null;
        if (index == 1)
        {
            heartImage = Player1Health;
        }
        else
        {
            heartImage = Player2Health;
        }

        heartImage[health - 1].enabled = false;
        
    }
}
