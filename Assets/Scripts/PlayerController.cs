using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public Rigidbody2D physics;
    public Transform gunArm;
    public Animator animator;

    public GameObject bulletToFire;
    public Transform muzzlePoint;

    public float timeBetweenShots;

    private Vector2 _moveInput;
    private Camera _camera;

    private float shotTimer;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _camera = Camera.main;
    }

    void Update() 
    {
        move();
        aim();
        shoot();
    }

    private void move()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");

        physics.velocity = _moveInput.normalized * moveSpeed;

        bool isMoving = _moveInput != Vector2.zero;
        animator.SetBool("isMoving", isMoving);
    }

    private void aim()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = _camera.WorldToScreenPoint(transform.localPosition);
        Vector3 offset = mousePosition - screenPoint;

        if (Mathf.Sign(offset.x) <= 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            gunArm.localScale = Vector3.one;
        }

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void shoot()
    {
        if (!Input.GetMouseButton(0))
        {
            return;
        }
        
        shotTimer -= Time.deltaTime;

        if (shotTimer > 0f)
        {
            return;
        }

        shotTimer = timeBetweenShots;
        Instantiate(bulletToFire, muzzlePoint.position, muzzlePoint.rotation);
    }
}
