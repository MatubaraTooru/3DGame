using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject _particle;
    [SerializeField, Header("Rayのスタートポイント")] Transform[] _rayStartPoints;
    [SerializeField] float _maxDistance;
    [SerializeField] Transform[] _wayPoints;
    [SerializeField] NavMeshAgent _navMeshAgent;
    [SerializeField] HPManager _hpManager;
    int _currentWaypointIndex;
    float _saveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent.SetDestination(_wayPoints[0].position);
        _saveSpeed = _navMeshAgent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_hpManager.NowHP < 0)
        {
            Death();
        }
        sighting(false);
    }
    void sighting(bool b)
    {
        if (b)
        {
            Transform playerpos = GameObject.FindGameObjectWithTag("Player").transform;
            transform.forward = playerpos.position - this.transform.position;
            _navMeshAgent.speed = 0;
        }
        else
        {
            _navMeshAgent.speed = _saveSpeed;
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _wayPoints.Length;
                _navMeshAgent.SetDestination(_wayPoints[_currentWaypointIndex].position);
            }
        }
    }

    void Death()
    {
        Instantiate(_particle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
