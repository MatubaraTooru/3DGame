using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] int _hp = 10;
    [SerializeField] GameObject _particle;
    public int _nowHP { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        _nowHP = _hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (_nowHP < 0)
        {
            Debug.Log("Die");
            _nowHP = _hp;
            //Instantiate(_particle, transform.position, Quaternion.identity);
            //Destroy(gameObject);
        }
    }
}
