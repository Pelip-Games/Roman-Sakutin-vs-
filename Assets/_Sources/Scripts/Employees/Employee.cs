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
    [SerializeField] private GoAway _goAway;
    [SerializeField] private Phrases _phrases;
    [SerializeField] private MoneyHunter _moneyHunter;
    [SerializeField] private EmployeeAnimator _animator;

    private bool _delayActive;
    private Coroutine _delay;
    
    private void Awake()
    {
        _patrolling.Init(_agent);
        _playerSeeker.Init(_player);
        _stalker.Init(_player, _agent);
        _goAway.Init(_agent);
        
        _patrolling.Enable();
        _animator.Walk();
    }

    private void OnEnable()
    {
        _playerSeeker.PlayerBecameVisible += OnPlayerBecameVisible;
        _playerSeeker.PlayerBecameInvisible += OnPlayerBecameInvisible;
        _moneyHunter.Hunted += OnHunted;
    }

    private void OnDisable()
    {
        _playerSeeker.PlayerBecameVisible -= OnPlayerBecameVisible;
        _playerSeeker.PlayerBecameInvisible -= OnPlayerBecameInvisible;
        _moneyHunter.Hunted -= OnHunted;
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
            _animator.Run();
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
        _animator.Walk();
        _delayActive = false;
    }

    private void OnHunted()
    {
        MoneyTaken();
    }

    [ContextMenu(nameof(MoneyTaken))]
    private void MoneyTaken()
    {
        _phrases.SayMoneyPhrase();
        _animator.GetCash();
        
        Vector3 direction = transform.position - _player.position;
        direction.z = 0;
        direction.Normalize();
        _goAway.Go(direction);
        
        _goAway.Gone += OnGone;
    }

    private void OnGone()
    {
        _goAway.Gone -= OnGone;
        Destroy(gameObject);
    }
}