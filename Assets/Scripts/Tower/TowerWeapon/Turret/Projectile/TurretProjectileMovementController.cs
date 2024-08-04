using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectileMovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + transform.forward * (movementSpeed * Time.fixedDeltaTime));
    }
}
