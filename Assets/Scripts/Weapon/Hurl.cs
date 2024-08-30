using Plugins.Audio.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hurl : Weapon
{
    [SerializeField] private GameObject bloodImpact;
    [SerializeField] private Directions directions = Directions.Normal;

    private ZombieAI _currentZombie;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _muzzle = GetComponent<Muzzle>();

        StartCoroutine(GetOut());
    }

    public override bool TryAction()
    {
        if(!_canAction)
            return false;
        
        StartCoroutine(Hit());
        return true;
    }

    protected override IEnumerator GetOut()
    {
        _animator.SetTrigger("GetOut");
        _muzzle.GetOutEffect();

        yield return new WaitForSeconds(getOutTime);

        _canAction = true;
    }

    private IEnumerator Hit()
    {
        _canAction = false;

        _animator.SetTrigger("Hit");

        GetComponent<SourceAudio>().PlayOneShot("BeatHit");

        yield return new WaitForSeconds(actionTime / 3);


        if(_currentZombie != null && !_currentZombie.IsDead)
        {
            if(_currentZombie.GetComponent<HealthLogic>())
            {
                var impact = Instantiate(bloodImpact, transform.position, Quaternion.identity);
                Destroy(impact, 2f);
                _currentZombie.GetComponent<HealthLogic>().TakeDamage(damage);
            }

            if(_currentZombie.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(Camera.main.transform.forward * force);
            }
        }
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, range, layer))
        {
            if(hit.collider.GetComponentInParent<ZombieAI>())
            {
                var zombie = hit.collider.GetComponentInParent<ZombieAI>();

                if(zombie.IsDead && hit.collider.TryGetComponent(out Rigidbody rb))
                {
                    Vector3 direction = Vector3.zero;

                    switch(directions)
                    {
                        case(Directions.Normal):
                        {
                            direction = -hit.normal;
                            break;
                        }
                        case(Directions.Up):
                        {
                            direction = Vector3.up;
                            break;
                        }
                        case(Directions.Forward):
                        {
                            direction = Camera.main.transform.forward;
                            break;
                        }
                    }

                    var impact = Instantiate(bloodImpact, hit.point, Quaternion.identity);
                    Destroy(impact, 2f);

                    rb.AddForce(direction * force);
                }
            }
        }
        

        yield return new WaitForSeconds(actionTime / 3 * 2);

        _canAction = true;
    }

    public override IEnumerator Hide()
    {
        _canAction = false;

        _animator.SetTrigger("Hide");

        yield return new WaitForSeconds(hideTime);

        WeaponEndHidding();
    }

    public void ZombieInRange(ZombieAI zombie)
    {
        _currentZombie = zombie;
        Debug.Log(zombie.name);
    }

    public void ZombieOutOfRange()
    {
        _currentZombie = null;
        Debug.Log("ZombieOut");
    }
}
public enum Directions
{
    Normal,
    Up, 
    Forward,
}

