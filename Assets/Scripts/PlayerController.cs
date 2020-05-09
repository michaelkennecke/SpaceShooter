using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //CharacterController characterController;
    Transform _transform;
    Camera _camera;
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private float projectileForce = 7.0f;
    public GameObject projectilePrefab;
    private float cooldownTimer;
    [SerializeField] private float cooldownTime = 1.0f;
    
    void Start()
    {
        this._transform = transform;
        this._camera = Camera.main;
    }

    //Standard UpdateLoop (once per Frame)
    void Update()
    {
        this.Rotate();
        this.MoveCharacter2();
        this.Shoot();
        this.Cooldown();
    }

    void Rotate()
    {
        Vector2 mousePos = this._camera.ScreenToWorldPoint(Input.mousePosition);
        float angleRad = Mathf.Atan2(mousePos.y - this._transform.position.y, mousePos.x - this._transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;
        this._transform.rotation = Quaternion.Euler(0, 0, angleDeg - 90);//diese -90 sind nötig für Sprites, die nach oben zeigen. Nutzen Sie andere Assets, könnte es sein, dass die das anpassen müssen
    
    }

    void MoveCharacter()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        this._transform.position += moveDirection * _speed * Time.deltaTime;
    }

    // Alternative Movement-Method (Not that good)
    void MoveCharacter2()
    {
        if (Input.GetKey(KeyCode.W)) 
        {
            this._transform.position += Vector3.up * _speed * Time.deltaTime; 
        }
        if (Input.GetKey(KeyCode.S))
        {
            this._transform.position += Vector3.down * _speed * Time.deltaTime; 
        }
        if (Input.GetKey(KeyCode.A)) 
        {
            this._transform.position += Vector3.left * _speed * Time.deltaTime; 
        }
        if (Input.GetKey(KeyCode.D))
        {
            this._transform.position += Vector3.right * _speed * Time.deltaTime;  
        }
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && cooldownTimer == 0)
        {
            // Instantiate a projectile GameObject with the same position and direction as the player
            GameObject projectileClone = Instantiate(projectilePrefab, this._transform.position, this._transform.rotation);
            // Use the gameObjects rigidbody to move the projectile
            Rigidbody2D rb = projectileClone.GetComponent<Rigidbody2D>();
            // Actual movement through a Force on the rb of the gameObject (projectile)
            rb.AddForce(this._transform.up * projectileForce, ForceMode2D.Impulse);
            // Makes that the use can only shoot after a certain cooldownTime
            cooldownTimer = cooldownTime; 
        }
    }

    void Cooldown()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        if (cooldownTimer < 0)
        {
            cooldownTimer = 0;
        }  
    }
}
