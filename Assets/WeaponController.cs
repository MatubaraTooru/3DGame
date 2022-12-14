using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField, Header("�e���̈ʒu")] Transform _muzzle;
    [SerializeField, Header("�N���X�w�A�̉摜")] Image _crosshair;
    [SerializeField, Header("�˒�����")] float _shootRange;
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField, Header("Ray�Ō��m���郌�C���[")] LayerMask _layer;

    /// <summary> Ray�Ō��m�����I�u�W�F�N�g�̈ʒu��ۑ����Ă����ϐ� </summary>
    Vector3 _hitposition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(_crosshair.rectTransform.position);
        RaycastHit hit;
        _hitposition = _muzzle.position + _muzzle.forward * _shootRange;

        if (Physics.Raycast(ray, out hit, _shootRange, _layer))
        {
            _crosshair.color = Color.red;
            _hitposition = hit.point;
        }
        else
        {
            _crosshair.color = Color.green;
        }

        if (Input.GetButton("Fire1"))
        {
            DrawLaser(_hitposition);
        }
        else
        {
            DrawLaser(_muzzle.position);
        }

        Debug.DrawRay(_muzzle.position, _hitposition, Color.red);
    }

    public void Fire()
    {
        
    }

    void DrawLaser(Vector3 destiation)
    {
        Vector3[] positions = { _muzzle.position, destiation };
        _lineRenderer.positionCount = positions.Length;
        _lineRenderer.SetPositions(positions);
    }
}
