using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float projectileForce = 7.0f;
    [SerializeField] protected float shootingCooldownTime = 0.1f;
    [SerializeField] protected ObjectPool projectilePool;
    [SerializeField] protected Transform shootingPoint;
    protected float cooldownTimer;
    protected AudioSource shotAudio;

    void Start()
    {
        this.shotAudio = this.GetComponent<AudioSource>();   
    }

    void Update()
    {
        this.Cooldown();
    }

    public virtual void Shoot()
    {
        if (this.cooldownTimer == 0)
        {
            GameObject projectile = this.projectilePool.GetPooledGameObject();
            if (projectile == null) return;
            projectile.transform.position = this.shootingPoint.position;
            projectile.transform.rotation = this.shootingPoint.rotation;
            projectile.SetActive(true);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(this.shootingPoint.up * this.projectileForce, ForceMode2D.Impulse);
            this.cooldownTimer = this.shootingCooldownTime;
            this.shotAudio.Play();
        }
        else return; 
    }

    void Cooldown()
    {
        if (this.cooldownTimer > 0)
        {
            this.cooldownTimer -= Time.deltaTime;
        }
        if (this.cooldownTimer < 0)
        {
            this.cooldownTimer = 0;
        }
    }
}
