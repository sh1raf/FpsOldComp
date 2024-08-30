using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(Rigidbody))]
public class Grenade : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float radiusDamage;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private int damage;

    [SerializeField] private float acceleration;

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject muzzle;

    private Rigidbody _rb;
    private Collider _collider;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        _rb.isKinematic = true;
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }

    [Button]
    public void Launch()
    {
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _rb.isKinematic = false;

        _collider.enabled = true;
        muzzle.SetActive(true);

        StartCoroutine(Moving());
    }

    private IEnumerator Moving()
    {
        while(true)
        {
            _rb.AddForce(-transform.forward * acceleration);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.collider.tag == "Wall")
            Destroy(other.collider.gameObject);

        PushObjects();

        Destroy(gameObject);
    }

    private void PushObjects()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radiusDamage, layerMask);


        foreach(Collider collider in colliders)
        {
            if(collider.GetComponentInParent<HealthLogic>())
            {
                if(collider.GetComponentInParent<HealthLogic>().TryGetComponent(out HealthLogic entity))
                {
                    entity.TakeDamage(damage);
                }
            }
            
            if(collider.GetComponent<Rigidbody>())
            {
                if(collider.TryGetComponent(out Rigidbody rigidbody))
                {
                    Vector3 direction = (collider.transform.position - transform.position).normalized;
                    rigidbody.AddForce(direction * force, ForceMode.Impulse);
                }
            }

            if(collider.tag == "Wall")
                Destroy(collider.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radiusDamage);
    }
}
