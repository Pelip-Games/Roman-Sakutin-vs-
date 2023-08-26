using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Patrolling : MonoBehaviour
{
    [SerializeField] private Transform[] _patrollingPoints;
    [SerializeField] private float _reachDistance;

    private int _pointIndex = -1;
    private Transform _currentPoint;
    private Coroutine _patrolling;
    private NavMeshAgent _agent;

    private void NextPoint()
    {
        _pointIndex++;

        if (_pointIndex >= _patrollingPoints.Length)
            _pointIndex = 0;
        
        _currentPoint = _patrollingPoints[_pointIndex];
    }

    public void Init(NavMeshAgent agent)
    {
        _pointIndex = 0;
        _currentPoint = _patrollingPoints[_pointIndex];
        _agent = agent;
    }
    
    public void Enable()
    {
        Disable();

        _agent.SetDestination(_currentPoint.position);
        _patrolling = StartCoroutine(WayPatrolling());
    }

    public void Disable()
    {
        if (_patrolling != null)
            StopCoroutine(_patrolling);
    }

    private IEnumerator WayPatrolling()
    {
        while (true)
        {
            float distance = Vector3.Distance(_currentPoint.position, transform.position);
            if (distance <= _reachDistance)
            {
                NextPoint();
                _agent.SetDestination(_currentPoint.position);
            }
            
            yield return null;
        }
    }
}
