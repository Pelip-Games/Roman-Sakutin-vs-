using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Stalker : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _stopDistance;
    
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
            _agent.SetDestination(Vector3.Distance(transform.position, _target.position) > _stopDistance
                ? _target.position
                : transform.position);

            yield return null;
        }
    }
}