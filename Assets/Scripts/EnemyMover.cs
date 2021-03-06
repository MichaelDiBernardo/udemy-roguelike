﻿using UnityEngine;

// Base class for enemy movement scripts that takes care of things
// that every monster needs to do.
public abstract class EnemyMover : Sleeper
{    
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
        if (IsAsleep)
        {
            _physics.velocity = Vector2.zero;
            return;
        }

        MoveThisFrame();

        _animator.SetBool("isMoving", _physics.velocity != Vector2.zero);
    }
   
    protected abstract void MoveThisFrame();
}
