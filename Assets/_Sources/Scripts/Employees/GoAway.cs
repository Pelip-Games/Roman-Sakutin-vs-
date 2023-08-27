using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GoAway : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _duration;
    [SerializeField, Min(0f)] private float _speed;

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
        float deltaTime;
        
        while (timer < _duration)
        {
            deltaTime = Time.deltaTime;
            _agent.Move(deltaTime * _speed * direction);
            timer += deltaTime;
            
            yield return null;
        }
        
        Gone?.Invoke();
    }
}