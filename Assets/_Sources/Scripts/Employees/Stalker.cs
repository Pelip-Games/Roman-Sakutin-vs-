using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Stalker : MonoBehaviour
{
    private Transform _target;
    private Coroutine _following;
    private NavMeshAgent _agent;

    public void Init(Transform target, NavMeshAgent agent)
    {
        _target = target;
        _agent = agent;
    }
    
    public void Enable()
    {
        _following = StartCoroutine(Following());
    }

    public void Disable()
    {
        if (_following != null)
            StopCoroutine(_following);
    }

    private IEnumerator Following()
    {
        while (true)
        {
            _agent.SetDestination(_target.position);

            yield return null;
        }
    }
}