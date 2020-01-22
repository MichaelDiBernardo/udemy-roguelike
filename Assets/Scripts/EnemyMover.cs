using UnityEngine;

// Base class for enemy movement scripts that takes care of things
// that every monster needs to do.
public abstract class EnemyMover : MonoBehaviour
{
    public SpriteRenderer theBody;

    protected Rigidbody2D _physics;
    private Animator _animator;

    void Awake()
    {
        _physics = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        if (!theBody.isVisible || !PlayerController.instance.gameObject.activeInHierarchy)
        {
            _physics.velocity = Vector3.zero;
            return;
        }        
        _animator.SetBool("isMoving", MoveThisFrame());        
    }

    // Return true if the mover is moving this frame, false otherwise.
    // This determines when animation happens.
    protected abstract bool MoveThisFrame();
}
