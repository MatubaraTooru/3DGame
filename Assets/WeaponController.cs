using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField, Header("銃口の位置")] Transform _muzzle;
    [SerializeField, Header("クロスヘアの画像")] Image _crosshair;
    [SerializeField, Header("射程距離")] float _shootRange;
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField, Header("Rayで検知するレイヤー")] LayerMask _layer;

    /// <summary> Rayで検知したオブジェクトの位置を保存しておく変数 </summary>
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
