using UnityEngine;
using UnityEngine.AI;

public class EmployeeSpriteSwap : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private float _starXScale;
    private NavMeshAgent _agent;

    private void Update()
    {
        float velocity = _agent.velocity.x;

        if (Mathf.Approximately(velocity, 0f))
            return;

        Vector3 scale = _target.localScale;
        scale.x = _starXScale;

        if (velocity > 0f)
            scale.x *= -1f;
        
        _target.localScale = scale;
    }

    public void Init(NavMeshAgent agent)
    {
        _starXScale = _target.localScale.x;
        _agent = agent;
    }
}