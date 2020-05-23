using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosionParticle;
    private GameObject target;
    [SerializeField] float moveSpeed = 0.5f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f;
        this.rb.rotation = angle;
        this.moveDirection = direction;
    }

    private void FixedUpdate()
    {
        this.moveSpaceship(moveDirection);
    }

    private void moveSpaceship(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //gameObject.GetComponentInChildren<ParticleSystem>().Play();
            Explode();
            this.gameObject.SetActive(false);
        }
    }

    public void Explode()
    {
        GameObject explosion = Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity) as GameObject;
        explosion.GetComponent<ParticleSystem>().Play();
        Destroy(explosion, 3f);
    }

}
