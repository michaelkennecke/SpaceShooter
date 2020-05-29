using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultishotWeapon : Weapon
{
    [SerializeField] private const float angleStep = 0.1f;
    private int amountOfProjectiles = 3;
    public Transform shootingPoint2;
    public Transform shootingPoint3;

    public override void Shoot()
    {
        Transform[] shootingPoints = {shootingPoint, shootingPoint2, shootingPoint3 };
        if (this.cooldownTimer == 0)
        {
            for (int i = 0; i < amountOfProjectiles; i++)
            {
                GameObject tmpProjectile = this.projectilePool.GetPooledGameObject();
                if (tmpProjectile == null) return;
                tmpProjectile.transform.position = shootingPoints[i].position;
                tmpProjectile.transform.rotation = shootingPoints[i].rotation;
                tmpProjectile.SetActive(true);
                Rigidbody2D rb = tmpProjectile.GetComponent<Rigidbody2D>();
                rb.AddForce(shootingPoints[i].up * this.projectileForce, ForceMode2D.Impulse);
                this.shotAudio.Play();
            }
            this.cooldownTimer = this.shootingCooldownTime;
        }
        else return;
    }
    
}
