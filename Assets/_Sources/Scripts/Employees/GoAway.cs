using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GoAway : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _duration;

    private NavMeshAgent _agent;

    public event Action Gone;

    public void Init(NavMeshAgent agent)
    {
        _agent = agent;
    }

    public void Go(Vector3 direction)
    {
        StartCoroutine(Reaching(direction));
    }

    private IEnumerator Reaching(Vector3 direction)
    {
        float timer = 0f;
        
        while (timer < _duration)
        {
            _agent.Move(direction);
            timer += Time.deltaTime;
            
            yield return null;
        }
        
        Gone?.Invoke();
    }
}