using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _playerSpeed = 5.0f;
    Rigidbody _rb;
    float _x;
    float _z;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _x = Input.GetAxisRaw("Horizontal");
        _z = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        Vector3 move = new Vector3(_x, 0, _z).normalized;
        _rb.velocity = move * _playerSpeed;
    }
}
