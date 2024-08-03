using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

    private void OnEnable()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        SetTarget(new Vector3(0,0,-6));
    }

    public void SetTarget(Vector3 pos)
    {
        _navMeshAgent.SetDestination(pos);
    }
}
