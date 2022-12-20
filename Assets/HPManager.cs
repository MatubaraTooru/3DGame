using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    [SerializeField, Header("HP‚Ì—Ê")]float _hp;
    [SerializeField] bool _godmode = false;
    public float NowHP { get; set; }
    private void Awake()
    {
        NowHP = _hp;
    }
    private void Update()
    {
        if (_godmode && NowHP < 0)
        {
            NowHP = _hp;
        }
    }
}
