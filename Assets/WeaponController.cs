using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField, Header("銃口の位置")] Transform _muzzle;
    [SerializeField, Header("射程距離")] float _range;
    public float Range { get; private set; }
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField, Header("連射速度")] float _fireRate;
    [SerializeField, Header("一発のダメージ")] float _weaponDamage;
    Vector3 _hitposition;
    Collider _hitcollider;
    Coroutine _coroutine = null;
    void Start()
    {
        Range = _range;
    }

    public void Fire(Vector3 hitposition, Collider hitcollider)
    {
        if (hitposition == default)
        {
            _hitposition = _muzzle.position + _muzzle.forward * _range;
        }
        else
        {
            _hitposition = hitposition;
        }

        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(FireRoutine(_hitposition, hitcollider));
            Debug.Log("Start Coroutine");
        }
    }

    public void EndFire()
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

    IEnumerator FireRoutine(Vector3 hitposition, Collider hitcollider)
    {
        while (true)
        {
            Debug.Log("Coroutine Active");
            DrawLaser(hitposition);
            if (hitcollider.CompareTag("Enemy") || hitcollider.CompareTag("Player"))
            {
                hitcollider.GetComponent<HPManager>().NowHP -= _weaponDamage;
            }
            yield return new WaitForSeconds(0.03f);
            DrawLaser(_muzzle.position);
            yield return new WaitForSeconds(_fireRate);
        }
    }
}
