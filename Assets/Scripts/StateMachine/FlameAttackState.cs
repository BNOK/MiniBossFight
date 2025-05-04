using System.Collections;
using UnityEngine;

public class FlameAttackState : StateBase
{
    public GameObject flameFX;
    public float timeBeforeAttack;
    public float AttackDuration;

    public Vector3 AttackLocation;
    public bool reachedAttackPosition = false;
    public bool canAttack = false;
    public float currentSpeed;

    public float SphereCastLength = 35.0f;
    public float SphereCastRadius = 1.5f;


    public override void EnterState()
    {
        if(flameFX != null)
        {
            flameFX = Instantiate(flameFX, Vector3.zero, Quaternion.identity);
            flameFX.SetActive(false);
        }

        float xValue = Random.Range(xValues[0], xValues[1]);
        AttackLocation = new Vector3(xValue, yValues[0], zValues[0]);
        currentSpeed = speeds[1] * 2;
        canAttack = true;
    }

    public override void ExecuteState()
    {
        _body.transform.position = Vector3.MoveTowards(_body.transform.position, AttackLocation, currentSpeed * Time.deltaTime);
        reachedAttackPosition = Vector3.Distance(_body.transform.position, AttackLocation) < 0.01f;

        if(reachedAttackPosition && canAttack)
        {
            //Debug.Log("isAttacking !");
            StartCoroutine(FlameAttackCoroutine());
            canAttack = false;
        }
    }

    public override void ExitState()
    {
        StopCoroutine(FlameAttackCoroutine());
    }

    IEnumerator FlameAttackCoroutine()
    {
        yield return new WaitForSeconds(timeBeforeAttack);
        flameFX.SetActive(true);
        flameFX.transform.position = _body.transform.position;
        CheckPlayerHit(_body);
        yield return new WaitForSeconds(AttackDuration);
        flameFX.SetActive(false);
        
    }

    void CheckPlayerHit(Rigidbody body)
    {
        RaycastHit[] hits;
        // necessary line or the length is too short !
        SphereCastLength = Mathf.Abs(body.transform.position.z - zValues[1]);
        hits = Physics.SphereCastAll(body.transform.position, SphereCastRadius, body.transform.forward, SphereCastLength,~3);

        Debug.DrawLine(body.transform.position, body.transform.position + (body.transform.forward * SphereCastLength), Color.red,10.0f,false);

        foreach (RaycastHit hit in hits)
        {
            // send damage to player hit
            Debug.Log("Hit: " + hit.collider.name);
        }
    }
}
