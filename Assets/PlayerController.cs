using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 3;
    [SerializeField] Rigidbody _rb = default;
    [SerializeField] WeaponController _weaponController;
    [SerializeField, Header("銃口の位置")] Transform _muzzle;
    [SerializeField, Header("クロスヘアの画像")] Image _crosshair = default;
    [SerializeField] LineRenderer _lineRenderer;
    float _range;
    Vector3 _hitposition;
    Collider _hitcollider;
    Ray _ray;

    void Start()
    {
        _range = _weaponController.Range;
    }

    void Update()
    {
        Move();
        _ray = Camera.main.ScreenPointToRay(_crosshair.rectTransform.position);

        RaycastHit hit = default;
        _hitcollider = null;
        _hitposition = default;

        if (Physics.Raycast(_ray, out hit, _range))
        {
            _hitposition = hit.point;
            _hitcollider = hit.collider;
        }

        if (_hitcollider && _hitcollider.CompareTag("Enemy"))
        {
            _crosshair.color = Color.red;
        }
        else
        {
            _crosshair.color = Color.green;
        }

        if (Input.GetMouseButton(0))
        {
            _weaponController.Fire(_hitposition, _hitcollider);
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
