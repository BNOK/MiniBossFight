using System.Collections;
using UnityEngine;

public class RocketObject : MonoBehaviour
{
    public Rigidbody body;
    public PlayerController Target;
    public ExplosionScript explosionEffect;
    public RocketAttack parent;


    public float speed = 20.0f;
    public float LaunchForce = 15.0f;
    public float timeBeforeAttack = 1.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody>();
        if (body == null)
        {
            body = gameObject.AddComponent<Rigidbody>();
        }

        if(Target == null)
        {
            GetComponent<CapsuleCollider>().isTrigger = false;
        }

        StartCoroutine(RocketCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (body.isKinematic)
        {
            Vector3 Direction = Target.transform.position - transform.position;
            transform.up = Direction;
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Time.deltaTime * speed);
        }
    }

    IEnumerator RocketCoroutine()
    {
        LaunchInAir();
        yield return new WaitForSeconds(timeBeforeAttack);
        if(Target != null)
        {
            body.useGravity = false;
            body.isKinematic = true;
        }
        StopCoroutine(RocketCoroutine());
    }

    void LaunchInAir()
    {
        Vector3 LaunchDirection = new Vector3(Random.Range(-1.0f, 1.0f), 1.0f, 0.5f);

        body.AddForce(LaunchDirection * LaunchForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ground")
        {
            if (Target == null) return;

            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            //other.gameObject.GetComponent<PlayerController>().TakeDamage();

            //Destroy(explosionFX);
            parent.rocketQueue.Enqueue(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
