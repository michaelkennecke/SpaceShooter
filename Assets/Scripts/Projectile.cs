using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   
    void OnEnable()
    {
        Invoke("Destroy", 5f);
    }

    void OnDisable()
    {
        CancelInvoke();   
    }

    void Destroy()
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            this.Destroy();
            //other.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            other.gameObject.GetComponent<Enemy>().Hit();
            //other.gameObject.SetActive(false);
        }
    }
}
