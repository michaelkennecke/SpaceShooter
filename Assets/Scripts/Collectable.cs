using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject multiShotWeapon = this.gameObject.transform.GetChild(0).gameObject;
            multiShotWeapon.transform.parent = other.transform;
            Destroy(this.gameObject);
        }
    }
}
