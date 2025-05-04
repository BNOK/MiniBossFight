using UnityEngine;

public abstract class StateBase : MonoBehaviour
{
    public static Rigidbody _body;

    // INIT min max values for 3 axes
    public float[] xValues = new float[] { -20.0f, 20.0f };
    public float[] yValues = new float[] { 2.0f, 12.0f };
    public float[] zValues = new float[] { -35.0f, 0.0f };

    public float[] speeds = new float[] { 10.0f, 20.0f };

    public PlayerController[] playerRefs;
    public virtual void Setup(Rigidbody body, PlayerController[] players)
    {
        _body = body;
        playerRefs = players;
    }

    public virtual void EnterState() { }
    public virtual void ExecuteState() { }
    public virtual void ExitState() { }

    //public void SiwtchStates(StateBase currentState, StateBase nextState)
    //{

    //}
}
