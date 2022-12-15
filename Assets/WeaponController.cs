using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField, Header("�e���̈ʒu")] Transform _muzzle = default;
    [SerializeField, Header("�N���X�w�A�̉摜")] Image _crosshair;
    [SerializeField, Header("�˒�����")] float _shootRange;
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField, Header("Ray�Ō��m���郌�C���[")] LayerMask _layer;
    [SerializeField, Header("�A�ˑ��x")] float _fireRate;
    [SerializeField, Header("�U���Ώۂ̃^�O"), TagField] string _enemyTag;
    Vector3 _hitposition = default;
    Collider _hitCollider = default;
    Coroutine _coroutine = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(_crosshair.rectTransform.position);
        RaycastHit hit = default;
        _hitposition = _muzzle.position + _muzzle.forward * _shootRange;

        if (Physics.Raycast(ray, out hit, _shootRange, _layer))
        {
            _crosshair.color = Color.red;
            _hitposition = hit.point;
            _hitCollider = hit.collider;
        }
        else
        {
            _crosshair.color = Color.green;
        }

        if (Input.GetButton("Fire1"))
        {
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(FireRoutine());
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
            Debug.Log("Stop Coroutine");
        }
        else
        {
            DrawLaser(_muzzle.position);
        }
    }

    public void Fire(Collider cl)
    {
        if (cl)
        {
            cl.GetComponent<EnemyController>()._nowHP -= 1;
            Debug.Log("Attack Hit");
        }
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
            Fire(_hitCollider);
            DrawLaser(_hitposition);
            yield return new WaitForSeconds(_fireRate);
        }
    }
}
