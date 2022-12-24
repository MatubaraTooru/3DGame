﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 3;
    [SerializeField] Rigidbody _rb = default;
    [SerializeField] WeaponController _weaponController;
    [SerializeField] Image _crosshair;
    Ray _ray;

    void Start()
    {
        
    }

    void Update()
    {
        Move();
        _ray = Camera.main.ScreenPointToRay(_crosshair.rectTransform.position);

        if (Input.GetMouseButton(0))
        {
            _weaponController.Fire();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _weaponController.EndFire();
        }
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = Vector3.forward * v + Vector3.right * h;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        if (dir != Vector3.zero) this.transform.forward = dir;
        dir = dir.normalized * _moveSpeed;
        float y = _rb.velocity.y;

        _rb.velocity = dir * _moveSpeed + Vector3.up * y;
    }
}
