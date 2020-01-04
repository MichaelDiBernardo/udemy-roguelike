using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float moveSpeed;

    private Vector3 _target;

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {        
    }

    void Update()
    {
        if (_target == null)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            _target,
            moveSpeed * Time.deltaTime
        );        
    }

    // Directs the camera to the target, which causes it to start moving towards it.
    // The Z axis will remain fixed (i.e. the target's Z is ignored.)
    public void SetTarget(Vector3 target)
    {
        _target = new Vector3(target.x, target.y, transform.position.z);
    }
}
