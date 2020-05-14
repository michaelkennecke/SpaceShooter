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
}
