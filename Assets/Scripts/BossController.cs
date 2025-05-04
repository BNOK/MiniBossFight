using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BossController : MonoBehaviour
{
    public Rigidbody bossBody;

    public int state = 1; // 0 moving, 1 flame attack, 2 rockets, 3 slam
    //boss properties
    public float xMinBounds = -20.0f;
    public float xMaxBounds = 20.0f;

    public float yMinBound = 2.0f;
    public float yMaxBound = 12.0f;

    public float zMinBound = -35.0f;
    public float zMaxBound = 0.0f;

    public bool isFlying = false;
    public bool hasAttacked = false;

    public float minSpeed = 10.0f;
    public float maxSpeed = 20.0f;
    private float currentSpeed;

    //move state properties
    public Vector3 targetMovePosition;
    public bool reachedMovePosition = false;
    public bool coroutineStarted = false;
    public float moveWaitTime = 5.0f;

    //flame attack properties
    public Vector3 targetAttackPosition = Vector3.zero;
    public bool reachedAttackPosition = false;
    public float TimebeforeAttack = 0.5f;
    public float AttackDuration = 2.5f;
    public GameObject flameFX;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetMovePosition = transform.position;
        reachedMovePosition = true;
        bossBody = GetComponent<Rigidbody>();

        switch (state)
        {
            case 0:
                StartCoroutine(MoveCoroutine());
                break;
            case 1:
                SetupFlameAttack();
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("state: " + state);
        switch (state)
        {
            case 0:
                Debug.Log("is moving : " + targetMovePosition);
                MoveBoss(currentSpeed, targetMovePosition, reachedMovePosition);
                break;
            case 1:
                Debug.Log("flame attack : " + targetAttackPosition);
                MoveToShootPosition();
                break;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            state = 1;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            state = 0;
        }

        isFlying = transform.position.y >= 5.0f;
        
    }

    #region Moving
    IEnumerator MoveCoroutine()
    {
        while (state == 0)
        {
            yield return new WaitForSeconds(Random.Range(0, moveWaitTime));
            Debug.Log("move coroutine is on !");
            getNewPosition(out targetMovePosition);
        }
    }
    void MoveBoss(float speed, Vector3 target, bool designatedtarget)
    {
        designatedtarget = Vector3.Distance(transform.position, target) < 0.1f;
        if (!designatedtarget)
        {   
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        
    }

    void getNewPosition( out Vector3 target)
    {
        float xValue = Random.Range(xMinBounds, xMaxBounds);
        float yValue = Random.Range(yMinBound, yMaxBound);

        currentSpeed = Random.Range(minSpeed, maxSpeed);

        target = new Vector3(xValue, yValue, zMinBound);
    }

    #endregion

    #region FlameAttack
    void SetupFlameAttack()
    {
        flameFX= Instantiate(flameFX, Vector3.zero, Quaternion.identity);
        //flameFX.transform.SetParent(transform, false);
        flameFX.SetActive(false);
        Debug.Log("flame attack setup");
        float xValue = Random.Range(xMinBounds, xMaxBounds);
        targetAttackPosition = new Vector3(xValue, yMinBound, zMinBound);
        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void MoveToShootPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetAttackPosition, currentSpeed * Time.deltaTime);
        reachedAttackPosition = Vector3.Distance(transform.position, targetAttackPosition) < 0.1f ;

        if (reachedAttackPosition && !hasAttacked)
        {
            StartCoroutine(FlameCoroutine());
            hasAttacked = true;
        }
    }

    IEnumerator FlameCoroutine()
    {
        yield return new WaitForSeconds(TimebeforeAttack);
        flameFX.transform.position = transform.position;
        flameFX.SetActive(true);
        Debug.Log("flame active !");
        yield return new WaitForSeconds(AttackDuration);
        Debug.Log("flame deactive !");
        flameFX.SetActive(false);
        
        StopCoroutine(FlameCoroutine());
        state = 0;
    }

    #endregion
}
