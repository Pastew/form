using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour {

    public float boomForce = 100f;
    public GameObject explosionPrefab;
    public bool dieOnExplosion = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<JelloBody>())
        {
            Camera.main.GetComponent<MyCamera>().Shake();

            other.GetComponent<JelloBody>().AddImpulse((other.transform.position - transform.position).normalized * boomForce * transform.localScale.x);

            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                GetComponent<AudioSource>().Play();
            }
            else
            {
                Debug.LogWarning("Explosive with no explosion prefab set on it");
            }

            other.GetComponentInParent<Player>().Die();

            if (dieOnExplosion)
                Destroy(gameObject, 0.1f);
        }
    }
}
