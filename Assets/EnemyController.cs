using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject _particle;
    [SerializeField, Header("Rayのスタートポイント")] Transform _rayStartPoint;
    [SerializeField] float _maxDistance;
    [SerializeField] Transform[] _wayPoints;
    [SerializeField] NavMeshAgent _navMeshAgent;
    [SerializeField] HPManager _hpManager;
    [SerializeField] Animator _animator;
    int _currentWaypointIndex;
    bool _detected;
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent.SetDestination(_wayPoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (_hpManager.NowHP <= 0)
        {
            Destroy(gameObject);
        }
        Detect();
    }
    void Detect()
    {
        Ray ray = new Ray(_rayStartPoint.position, transform.forward);
        Debug.DrawRay(_rayStartPoint.position, transform.forward, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                _detected = true;
            }
            else
            {
                _detected = false;
            }
        }

        if (_detected)
        {
            _navMeshAgent.isStopped = true;

        }
        else
        {
            _navMeshAgent.isStopped = false;
            if (_navMeshAgent.remainingDistance < _navMeshAgent.stoppingDistance)
            {
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _wayPoints.Length;
                _navMeshAgent.SetDestination(_wayPoints[_currentWaypointIndex].position);
            }
        }
    }

    private void OnDestroy()
    {
        GameObject particle = Instantiate(_particle, transform.position, Quaternion.identity);
        Destroy(particle, 1f);
    }
}
