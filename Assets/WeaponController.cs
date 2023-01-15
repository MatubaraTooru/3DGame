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
    [SerializeField, Header("弾倉の大きさ")] int _magsize;
    [SerializeField] Text _magText;
    [SerializeField] float _reloadSpeed;
    float _reloadTimer;
    int _remainingammo;
    Vector3 _hitposition;
    Collider _hitcollider;
    Ray _ray;
    float _rateTimer;
    void Start()
    {
        _remainingammo = _magsize;
        _rateTimer = _fireRate;
        _magText.text = _magsize.ToString();
    }
    private void Update()
    {
        _rateTimer += Time.deltaTime;
        _magText.text = $"{_remainingammo} / {_magsize}";

        Sight();
        if (Input.GetMouseButton(0) && _reloadTimer <= 0)
        {
            if (_rateTimer > _fireRate && _remainingammo > 0)
            {
                DrawLaser(_hitposition);
                Attack();
                _remainingammo--;
                _rateTimer = 0;
            }
            else
            {
                DrawLaser(_muzzle.position);
            }
        }
        else
        {
            DrawLaser(_muzzle.position);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
            _reloadTimer += Time.deltaTime;
            if (_reloadTimer > _reloadSpeed)
            {
                _reloadTimer = 0;
            }
        }
    }
    void Sight()
    {
        _ray = Camera.main.ScreenPointToRay(_crosshair.rectTransform.position);

        _hitcollider = null;
        _hitposition = _muzzle.position + _muzzle.forward * _range;

        if (Physics.Raycast(_ray, out RaycastHit hit, _range))
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
    }

    void Attack()
    {
        if (_hitcollider && _hitcollider.CompareTag("Enemy"))
        {
            _hitcollider.GetComponent<HPManager>().NowHP -= _weaponDamage;
        }
    }
    void Reload()
    {
        _remainingammo = _magsize;
    }

    void DrawLaser(Vector3 destiation)
    {
        Vector3[] positions = { _muzzle.position, destiation };
        _lineRenderer.positionCount = positions.Length;
        _lineRenderer.SetPositions(positions);
    }
}
