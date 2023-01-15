using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 3;
    [SerializeField] Rigidbody _rb;
    [SerializeField] Slider _hpSlider;
    [SerializeField] HPManager _hpManager;
    float _h;
    float _v;

    void Start()
    {
        Cursor.visible = false;
        _hpSlider.maxValue = _hpManager.NowHP;
    }

    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");

        _hpSlider.value = _hpManager.NowHP;
    }
    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 dir = Vector3.forward * _v + Vector3.right * _h;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        if (dir != Vector3.zero) this.transform.forward = dir;
        dir = dir.normalized * _moveSpeed;
        float y = _rb.velocity.y;

        _rb.velocity = dir * _moveSpeed + Vector3.up * y;
    }
}
