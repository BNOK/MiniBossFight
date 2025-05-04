using UnityEngine;
using System.Collections;

public class MoveState : StateBase
{
    public bool reachedTarget = false;

    public float currentSpeed;
    public Vector3 targetLocation;

    public float moveWaitTime = 1.5f;

    public override void EnterState()
    {
        float xValue = Random.Range(xValues[0], xValues[1]);
        float yValue = Random.Range(yValues[0], yValues[1]);

        currentSpeed = Random.Range(speeds[0], speeds[1]);

        targetLocation = new Vector3(xValue, yValue, zValues[0]);
        //Debug.Log("location setup ! : " + targetLocation);
        StartCoroutine(MoveCoroutine());

    }

    public override void ExecuteState()
    {   
        _body.transform.position = Vector3.MoveTowards(_body.transform.position, targetLocation, currentSpeed * Time.deltaTime);
        reachedTarget = Vector3.Distance(_body.transform.position, targetLocation) < 0.01f;   
    }

    public override void ExitState()
    {
        StopCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0, moveWaitTime));
            //Debug.Log("move coroutine is on !");

            currentSpeed = Random.Range(speeds[0], speeds[1]);
            setTargetLocation();
        }
    }

    void setTargetLocation()
    {
        float xValue = Random.Range(xValues[0], xValues[1]);
        float yValue = Random.Range(yValues[0], yValues[1]);

        targetLocation = new Vector3(xValue, yValue, zValues[0]);
    }

}
