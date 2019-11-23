using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D physics;

    public Transform gunArm;

    private Vector2 _moveInput;

    void Start()
    {
    }

    void Update()
    {
        move();
        aim();
    }

    private void move()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");

        physics.velocity = _moveInput * moveSpeed;
    }

    private void aim()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        Vector3 offset = mousePosition - screenPoint;

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
