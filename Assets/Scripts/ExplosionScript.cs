using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<ParticleSystem>().Play();

        Debug.Log("collision detected ! ");
        string objectTag = other.gameObject.tag;

        if (objectTag.Contains("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(1);
            Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);

        }
    }

}
