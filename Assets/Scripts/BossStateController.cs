using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(LineRenderer))]
public class BossStateController : MonoBehaviour
{

    public PlayerController[] playerReferences;


    // State Init
    public MoveState moveState;
    public FlameAttackState flameAttackState;
    public RocketAttack RocketAttack;

    public StateBase currentState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetupBoss(PlayerController[] playerArray)
    {
        //playerReferences = GameObject.FindObjectsByType<PlayerController>(FindObjectsSortMode.InstanceID);
        playerReferences = playerArray;


        moveState.Setup(GetComponent<Rigidbody>(), playerReferences);
        flameAttackState.Setup(GetComponent<Rigidbody>(), playerReferences);
        RocketAttack.Setup(GetComponent<Rigidbody>(), playerReferences);

        currentState = moveState;
        currentState.EnterState();

        StartCoroutine(SwitchState());
    }

    // this is for debugging
    IEnumerator SwitchState()
    {
        currentState.ExitState();
        //Debug.Log("Exited the move state");
        yield return new WaitForSeconds(10.0f);
        currentState = flameAttackState;
        currentState.EnterState();
        //Debug.Log("entered flame attack state");
        yield return new WaitForSeconds(10.0f);
        currentState.ExitState();
        currentState = RocketAttack;
        currentState.EnterState(); 
    }

    // Update is called once per frame
    void Update()
    {
        currentState.ExecuteState();
    }
}
