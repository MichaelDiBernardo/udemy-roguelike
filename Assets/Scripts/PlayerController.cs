using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;

    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;
    public float dashInvincibility;

    public Rigidbody2D physics;
    public Transform gunArm;
    public Animator animator;
    public SpriteRenderer bodyRenderer;

    public GameObject bulletToFire;
    public Transform muzzlePoint;

    public float timeBetweenShots;

    private Vector2 _moveInput;
    private Camera _camera;

    private FrameTimer _shotTimer;
    private FrameTimer _dashTimer;
    private FrameTimer _dashCooldownTimer;

    private float _currentMoveSpeed;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _camera = Camera.main;
        _shotTimer = new FrameTimer(timeBetweenShots, true);
        _dashCooldownTimer = new FrameTimer(dashCooldown, true);
        _currentMoveSpeed = moveSpeed;
    }

    void Update() 
    {
        dash();
        move();
        aim();
        shoot();
    }

    private void dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _dashTimer == null && _dashCooldownTimer.CheckThisFrame())
        {
            _currentMoveSpeed = dashSpeed;
            _dashTimer = new FrameTimer(dashDuration, false);
            animator.SetTrigger("dash");
        }

        if (_dashTimer != null && _dashTimer.CheckThisFrame())
        {
            _currentMoveSpeed = moveSpeed;
            _dashTimer = null;
            _dashCooldownTimer.Reset();            
        }
    }

    private void move()
    {
        if (_dashTimer == null)
        {
            _moveInput.x = Input.GetAxisRaw("Horizontal");
            _moveInput.y = Input.GetAxisRaw("Vertical");
        }

        physics.velocity = _moveInput.normalized * _currentMoveSpeed;

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
            _shotTimer.Reset();
            return;
        }

        if (_shotTimer.CheckThisFrame())
        {
            Instantiate(bulletToFire, muzzlePoint.position, muzzlePoint.rotation);
        }        
    }
}
