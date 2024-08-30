using UnityEngine;
using NaughtyAttributes;
using UnityEngine.AI;
using System.Collections;
using Zenject;
using UnityEditor;
using System.ComponentModel;
using Plugins.Audio.Core;

[RequireComponent(typeof(Animator))]
public class ZombieAI : MonoBehaviour
{
    [SerializeField] private float distanceToStartHunting;
    [SerializeField] private int damage;

    [SerializeField] private float attackTime;
    [SerializeField] private Transform centerOfBody;

    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    [SerializeField] private SourceAudio source;

    public bool IsDead{get{return _isDead;} private set{}}

    private Player _player;

    private Animator _animator;
    private NavMeshAgent _agent;

    private bool _isHunting = false;
    private bool _isAttacking = false;

    private bool _playerInAttackRange = false;

    private bool _isDead = false;


    private void Start()
    {
        GetComponentInChildren<Canvas>().enabled = false;

        _player = FindObjectOfType<Player>();

        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = Random.Range(minSpeed, maxSpeed);

        source.Play("Scream");
        source.Pitch = Random.Range(0.8f, 1.1f);

        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent(typeof(SphereCollider));
            SphereCollider sphere = GetComponent<SphereCollider>();
            sphere.radius = distanceToStartHunting;
            sphere.isTrigger = true;
        }

        foreach(Rigidbody child in GetComponentsInChildren<Rigidbody>())
        {
            child.useGravity = false;
            child.isKinematic = true;
        }
    }

    public void NeedHunting()
    {
        if(!_isHunting && !_isAttacking && !_isDead)
            StartCoroutine(Hunting());
    }
    

    private IEnumerator Hunting()
    {
        GetComponentInChildren<Canvas>().enabled = true;

        Debug.Log("HuntingStart");
        bool valueBool = true;
        while(valueBool)
        {
            if(!_agent.isActiveAndEnabled)
                valueBool = false;

            _agent.SetDestination(_player.transform.position);
            _animator.SetFloat("MoveSpeed", _agent.speed);


            if(_playerInAttackRange)
            {
                StartCoroutine(AttackAndChase());
                Debug.Log("StopHunting");
                _isHunting = false;
                valueBool = false;
            }

            Debug.Log("Hunting");
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator AttackAndChase()
    {
        bool value = true;

        while(value)
        {
            if(!_isAttacking && _playerInAttackRange)
            {
                StartCoroutine(Attact());
            }
            if(!_agent.isActiveAndEnabled)
                value = false;

            if(!_playerInAttackRange)
                _agent.SetDestination(_player.transform.position);

            yield return null;
        }
    }

    private IEnumerator Attact()
    {
        _isAttacking = true;

        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackTime / 4);

        if(_playerInAttackRange && !_isDead)
        {
            Debug.Log("Attack");
            _player.GetComponent<PlayerHealthLogic>().TakeDamage(damage);
        }

        yield return new WaitForSeconds(attackTime / 4 * 3);

        _isAttacking = false;
    }

    private void Patrol()
    {

    }

    [Button]
    public void RagdollOn()
    {
        GetComponentInChildren<Canvas>().enabled = false;

        foreach(Rigidbody child in GetComponentsInChildren<Rigidbody>())
        {
            child.isKinematic = false;
            child.useGravity = true;
        }
        StopAllCoroutines();
        _animator.StopPlayback();
        _animator.enabled = false;

        _agent.enabled = false;
        _isDead = true;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.GetComponent<Player>() && !_isHunting && !_isAttacking && !_isDead)
        {
            StartCoroutine(Hunting());
            _isHunting = true;
        }
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireSphere(centerOfBody.position, distanceToStartHunting);
    }

    public void PlayerInRange()
    {
        _playerInAttackRange = true;
    }

    public void PlayerOutOfRange()
    {
        _playerInAttackRange = false;
    }
}
