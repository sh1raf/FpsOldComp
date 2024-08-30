using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ZombieDetection : MonoBehaviour
{
    private Hurl _hurl;
    private ZombieAI _currentZombie;
    private void Awake() 
    {
        _hurl = GetComponentInParent<Hurl>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.GetComponentInParent<ZombieAI>() && !other.isTrigger)
        {
            if(other.GetComponentInParent<ZombieAI>().TryGetComponent(out ZombieAI zombie))
            {
                _currentZombie = zombie;
                _hurl.ZombieInRange(zombie);
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.GetComponentInParent<ZombieAI>() == _currentZombie)
        {
            _hurl.ZombieOutOfRange();
        }
    }
}
