﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] GameObject weapon;
    [SerializeField] int lifePoints = 3;
    private int selectedWeapon;
    Transform _transform;
    Camera _camera;
    public Vector2 moveDirection;
    private Rigidbody2D rb;

    void Start()
    {
        this._transform = transform;
        this._camera = Camera.main;
        selectedWeapon = 0;
        SelectWeapon();
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        this.Rotate();
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetButtonDown("Fire1"))
        {
            this.weapon.GetComponent<Weapon>().Shoot();
        }
        int previousSelectedWeapon = this.selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (this.selectedWeapon >= transform.childCount - 1)
            {
                this.selectedWeapon = 0;
            }
            else
            {
                this.selectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (this.selectedWeapon <= 0)
            {
                this.selectedWeapon = transform.childCount - 1;
            }
            else
            {
                this.selectedWeapon--;
            }
        }
        if (previousSelectedWeapon != this.selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void FixedUpdate()
    {
        MoveSpaceship(moveDirection);
    }

    void Rotate()
    {
        Vector2 mousePos = this._camera.ScreenToWorldPoint(Input.mousePosition);
        float angleRad = Mathf.Atan2(mousePos.y - this._transform.position.y, mousePos.x - this._transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;
        this._transform.rotation = Quaternion.Euler(0, 0, angleDeg - 90);//diese -90 sind nötig für Sprites, die nach oben zeigen. Nutzen Sie andere Assets, könnte es sein, dass die das anpassen müssen
    }

    void MoveSpaceship(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (moveDirection * this.speed * Time.deltaTime));
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                Debug.Log("Weapon " + selectedWeapon);
                this.weapon = weapon.gameObject;
            }
            i++;
        }
    }

    void LoseLifePoint()
    {
        this.lifePoints -= 1;
        if (this.lifePoints <= 0)
        {
            SceneController.Instance.ToEndMenu();
        }
    }

    public void Hit()
    {
        this.LoseLifePoint();
    }

}
