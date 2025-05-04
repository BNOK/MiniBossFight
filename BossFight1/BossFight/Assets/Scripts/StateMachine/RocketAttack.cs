using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class RocketAttack : StateBase
{
    public GameObject rocketPrefab;
    public LineRenderer lineRenderer;
    public float lineWidth = 0.1f;

    public float timebeforeAttack = 2.5f;

    public Queue<GameObject> rocketQueue = new Queue<GameObject>();

    public override void Setup(Rigidbody body, PlayerController[] players)
    {
        base.Setup(body, players);

        lineRenderer = body.gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 3;

        for (int i = 0; i < 2; i++)
        {
            SpawnRocket(playerRefs[i]);
        }
        SpawnRocket(null);
    }

    void SpawnRocket(PlayerController playerref)
    {
        GameObject lastrocket = Instantiate(rocketPrefab, Vector3.zero, Quaternion.identity);
        lastrocket.GetComponent<RocketObject>().Target = playerref;
        lastrocket.SetActive(false);
        lastrocket.GetComponent<RocketObject>().parent = this;
        rocketQueue.Enqueue(lastrocket);
    }

    public override void EnterState()
    {
        // fixed line width
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 3;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        StartCoroutine(RocketAttackCoroutine());
    }

    IEnumerator RocketAttackCoroutine()
    {
        
        yield return new WaitForSeconds(timebeforeAttack);
        lineRenderer.enabled = false;
        while (rocketQueue.Count > 0)
        {
            yield return new WaitForSeconds(0.5f);
            ShootRocket();
        }
    }

    void ShootRocket()
    {
        GameObject projectile = rocketQueue.Dequeue();
        projectile.transform.position = _body.transform.position;
        projectile.SetActive(true);
        
        
        
        //redirect rocket

    }

    public override void ExecuteState()
    {
        if (lineRenderer.enabled == true)
        {
            LineFollow();
        }
    }

    void LineFollow()
    {
        lineRenderer.SetPosition(0, playerRefs[0].transform.position);
        lineRenderer.SetPosition(1, _body.transform.position);
        lineRenderer.SetPosition(2, playerRefs[1].transform.position);
    }

    public override void ExitState()
    {
        StopCoroutine(RocketAttackCoroutine());
        if(lineRenderer.enabled == true)
        {
            lineRenderer.enabled = false;
        }
    }
}
