using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Employee : MonoBehaviour
{
    [SerializeField] private float _stalkeringStopDelay = 1f;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _player;
    [SerializeField] private Patrolling _patrolling;
    [SerializeField] private PlayerSeeker _playerSeeker;
    [SerializeField] private Stalker _stalker;
    [SerializeField] private Phrases _phrases;

    private bool _delayActive;
    private Coroutine _delay;
    
    private void Awake()
    {
        _patrolling.Init(_agent);
        _playerSeeker.Init(_player);
        _stalker.Init(_player, _agent);
        
        _patrolling.Enable();
    }

    private void OnEnable()
    {
        _playerSeeker.PlayerBecameVisible += OnPlayerBecameVisible;
        _playerSeeker.PlayerBecameInvisible += OnPlayerBecameInvisible;
    }

    private void OnDisable()
    {
        _playerSeeker.PlayerBecameVisible -= OnPlayerBecameVisible;
        _playerSeeker.PlayerBecameInvisible -= OnPlayerBecameInvisible;
    }

    private void OnPlayerBecameVisible()
    {
        if (_delayActive)
        { 
            StopCoroutine(_delay);
        }
        else
        {
            _patrolling.Disable();
            _stalker.Enable();
            _phrases.SayStalkerPhrase();
        }
    }

    private void OnPlayerBecameInvisible()
    {
        _delayActive = true;
        _delay = StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_stalkeringStopDelay);

        _stalker.Disable();
        _patrolling.Enable();
        _phrases.SayMissPhrase();
        _delayActive = false;
    }
}