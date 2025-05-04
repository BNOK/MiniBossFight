using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public InputAction Move;

    public int health = 3;
    
    
    void Start()
    {
        //Move = GetComponent<PlayerInput>().actions.FindAction("Move",true);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 move = Move.ReadValue<Vector2>();
        Debug.Log("input : ");
    }

    public void TakeDamage(int Damage)
    {
        if (health > 0)
        {
            health--;
        }
    }
}
