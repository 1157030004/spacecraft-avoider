using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if(playerHealth == null) return;

        playerHealth.Crash();
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
