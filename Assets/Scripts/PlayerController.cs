using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody rb;
    private Vector3 direction;
    public float speed = 5.0f;

    public int health = 3;
    public string horizontalName;
    public string verticalName;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Move = GetComponent<PlayerInput>().actions.FindAction("Move",true);
    }

    // Update is called once per frame
    void Update()
    {
        float vInput = Input.GetAxis(verticalName);
        float hInput = Input.GetAxis(horizontalName);

        direction = new Vector3(hInput, 0, vInput);
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * speed *Time.deltaTime);
    }

    public void TakeDamage(int Damage)
    {
        if (health > 0)
        {
            health--;
        }
    }
}
