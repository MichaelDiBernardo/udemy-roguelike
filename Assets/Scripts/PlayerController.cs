using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private Vector2 _moveInput;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(
            _moveInput.x * Time.deltaTime * moveSpeed,
            _moveInput.y * Time.deltaTime * moveSpeed,
            0f
        );
    }
}
