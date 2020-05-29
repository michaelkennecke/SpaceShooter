using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int gamePoints = 1;
    [SerializeField] int lifePoints = 2;
    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] bool randomMovement = false;
    [SerializeField] GameObject explosionParticle;
    [SerializeField] AudioClip explosionAudio;

    private Rigidbody2D target;
    private Rigidbody2D rb;
    private int lifeCounter;
    private Vector2 moveDirection;
    private Vector2 moveSpot;
    
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        lifeCounter = lifePoints;
    }

    private void FixedUpdate()
    {
        this.Rotate();
        if (!randomMovement)
        {
            this.rb.MovePosition(this.rb.position + (this.target.position - this.rb.position).normalized * Time.fixedDeltaTime * this.moveSpeed);
        }
        else
        {
            this.rb.MovePosition(this.rb.position + (this.moveSpot - this.rb.position).normalized * Time.fixedDeltaTime * this.moveSpeed);
            if (Vector2.Distance(this.moveSpot, this.rb.position) < 0.2f)
            {
                this.GetNewMoveSpot();
            }
        }
    }

    private void Rotate()
    {
        Vector2 playerPosition = target.position;
        float angleRad = Mathf.Atan2(playerPosition.y - this.rb.position.y, playerPosition.x - this.rb.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;
        this.rb.rotation = angleDeg - 90;
    }

    private void GetNewMoveSpot()
    {
        this.moveSpot = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10));
        this.moveDirection = this.moveSpot;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<AudioSource>().Play();
            col.gameObject.GetComponent<PlayerController>().Hit();
            this.Hit();
        }
    }

    public void Hit()
    {
        this.LoseLifePoint();
    }

    private void LoseLifePoint()
    {
        this.lifeCounter -= 1;
        if (this.lifeCounter <= 0)
        {
            this.Explode();
            Score.AddScore(this.gamePoints);
        }
    }

    private void Explode()
    {
        GameObject explosion = Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity) as GameObject;
        explosion.GetComponent<ParticleSystem>().Play();
        Destroy(explosion, 3f);
        AudioSource.PlayClipAtPoint(explosionAudio, Camera.main.transform.position);
        this.lifeCounter = lifePoints; 
        this.gameObject.SetActive(false);
    }
}
