using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField, Header("銃口の位置")] Transform _muzzle;
    [SerializeField, Header("射程距離")] float _range;
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField, Header("連射速度")] float _fireRate;
    [SerializeField, Header("一発のダメージ")] float _weaponDamage;
    [SerializeField, Header("クロスヘアの画像")] Image _crosshair;
    Vector3 _hitposition;
    Collider _hitcollider;
    Coroutine _coroutine = null;
    Ray _ray;
    void Start()
    {
        
    }
    private void Update()
    {
        _ray = Camera.main.ScreenPointToRay(_crosshair.rectTransform.position);

        RaycastHit hit = default;
        _hitcollider = null;
        _hitposition = _muzzle.position + _muzzle.forward * _range;

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
            Fire();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopFire();
        }
    }

    void Fire()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(FireRoutine());
            Debug.Log("Start Coroutine");
        }
    }

    void StopFire()
    {
        StopCoroutine(_coroutine);
        _coroutine = null;
        DrawLaser(_muzzle.position);
        Debug.Log("Stop Coroutine");
    }

    void DrawLaser(Vector3 destiation)
    {
        Vector3[] positions = { _muzzle.position, destiation };
        _lineRenderer.positionCount = positions.Length;
        _lineRenderer.SetPositions(positions);
    }

    IEnumerator FireRoutine()
    {
        while (true)
        {
            DrawLaser(_hitposition);
            if (_hitcollider.CompareTag("Enemy"))
            {
                _hitcollider.GetComponent<HPManager>().NowHP -= _weaponDamage;
            }
            yield return new WaitForSeconds(0.03f);
            DrawLaser(_muzzle.position);
            yield return new WaitForSeconds(_fireRate);
        }
    }
}
